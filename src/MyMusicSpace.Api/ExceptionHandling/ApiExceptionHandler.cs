using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyMusicSpace.Business.Exceptions;
using MyMusicSpace.Data.Exceptions;

namespace MyMusicSpace.Api.ExceptionHandling
{
    public sealed class ApiExceptionHandler : IExceptionHandler
    {
        private readonly IProblemDetailsService _problemDetailsService;
        private readonly ILogger<ApiExceptionHandler> _logger;

        public ApiExceptionHandler(
            IProblemDetailsService problemDetailsService,
            ILogger<ApiExceptionHandler> logger)
        {
            _problemDetailsService = problemDetailsService;
            _logger = logger;
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
    }
}
