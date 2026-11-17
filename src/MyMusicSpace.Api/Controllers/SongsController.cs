using Microsoft.AspNetCore.Mvc;
using MyMusicSpace.Business.Services.Abstractions;
using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Contracts;
using MyMusicSpace.Contracts.Dtos;

namespace MyMusicSpace.Api.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public sealed class SongsController : ControllerBase
    {
        private readonly ISongCreateService _songService;
        private readonly IProviderMusicSearchService _songSearchService;
        private readonly IProviderMusicGetByIdService _songGetByIdService;

        public SongsController(
            ISongCreateService songService,
            IProviderMusicSearchService songSearchService,
            IProviderMusicGetByIdService songGetByIdService)
        {
            _songService = songService;
            _songSearchService = songSearchService;
            _songGetByIdService = songGetByIdService;
        }

        [HttpGet("provider/search")]
        [ProducesResponseType(typeof(SongProviderSearchResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<SongProviderSearchResponseDto>> SearchProvider(
            [FromQuery] SongProviderSearchQueryDto query,
            CancellationToken cancellationToken)
        {
            var results = await _songSearchService.ExecuteAsync(query, cancellationToken);

            return Ok(results);
        }

        [HttpGet("provider/{providerId}")]
        [ProducesResponseType(typeof(SongProviderDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<SongProviderDto>> GetByProviderId(
            string providerId,
            CancellationToken cancellationToken)
        {
            var song = await _songGetByIdService.ExecuteAsync(providerId, cancellationToken);

            return Ok(song);
        }

        [HttpPost]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SongDto>> Create(
            [FromBody] SongCreateRequestDto request,
            CancellationToken cancellationToken)
        {
            var song = await _songService.ExecuteAsync(request, cancellationToken);

            return Created($"/api/songs/{song.Id}", song);
        }
    }
}
