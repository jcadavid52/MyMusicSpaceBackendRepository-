using Microsoft.AspNetCore.Mvc;
using MyMusicSpace.Business.Services.Abstractions;
using MyMusicSpace.Business.Services.Dtos;

namespace MyMusicSpace.Api.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public sealed class SongsController : ControllerBase
    {
        private readonly ISongCreateService _songService;
        private readonly ISongSearchService _songSearchService;

        public SongsController(
            ISongCreateService songService,
            ISongSearchService songSearchService)
        {
            _songService = songService;
            _songSearchService = songSearchService;
        }

        [HttpGet("search")]
        [ProducesResponseType(typeof(SongSearchResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status502BadGateway)]
        public async Task<ActionResult<SongSearchResponseDto>> Search(
            [FromQuery] SongSearchQueryDto query,
            CancellationToken cancellationToken)
        {
            var results = await _songSearchService.ExecuteAsync(query, cancellationToken);

            return Ok(results);
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
