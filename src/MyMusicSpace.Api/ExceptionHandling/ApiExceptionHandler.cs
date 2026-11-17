using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MyMusicSpace.Business.Exceptions;
using MyMusicSpace.Contracts.Exceptions;

namespace MyMusicSpace.Api.ExceptionHandling
{
    public sealed class ApiExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<ApiExceptionHandler> _logger;
        private readonly IHostEnvironment _environment;

        public ApiExceptionHandler(
            IProblemDetailsService problemDetailsService,
            ILogger<ApiExceptionHandler> logger,
            IHostEnvironment environment)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
            _environment = environment;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is OperationCanceledException && httpContext.RequestAborted.IsCancellationRequested)
            {
                return true;
            }

            var (statusCode, title, detail) = exception switch
            {
                SongAlreadyExistsException ex
                    => (StatusCodes.Status409Conflict, "Song already exists", ex.Message),

                UniqueConstraintViolationException
                    => (StatusCodes.Status409Conflict,
                        "Concurrent modification",
                        "Another request modified the same data. Try again."),

                ProviderMusicSongNotFoundException ex
                    => (StatusCodes.Status404NotFound,
                        "Song not found",
                        GetDetail(ex, "The requested song was not found in the music provider.")),

                ProviderMusicAuthenticationException ex
                    => (StatusCodes.Status502BadGateway,
                        "Music provider authentication failed",
                        GetDetail(ex, "Could not authenticate with the music provider.")),

                ProviderMusicException ex
                    => (StatusCodes.Status502BadGateway,
                        "Music provider error",
                        GetDetail(ex, "The music provider could not complete the request.")),

                _ => (StatusCodes.Status500InternalServerError,
                        "Internal server error",
                        "An unexpected error occurred.")
            };

            if (statusCode >= StatusCodes.Status500InternalServerError)
            {
                _logger.LogError(exception, "Unhandled exception");
            }
            else
            {
                _logger.LogWarning(exception, "Exception mapped to status code {StatusCode}", statusCode);
            }

            httpContext.Response.StatusCode = statusCode;

            return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Detail = detail
                }
            });
        }

        private string GetDetail(Exception exception, string productionDetail) =>
            _environment.IsDevelopment() ? exception.Message : productionDetail;
    }
}
