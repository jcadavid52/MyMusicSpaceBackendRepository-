using Microsoft.AspNetCore.Mvc;
using MyMusicSpace.Business.Services;
using MyMusicSpace.Business.Services.Dtos;

namespace MyMusicSpace.Api.Controllers
{
    [ApiController]
    [Route("api/songs")]
    public sealed class SongsController : ControllerBase
    {
        private readonly ISongService _songService;

        public SongsController(ISongService songService) => _songService = songService;

        [HttpPost]
        [ProducesResponseType(typeof(SongDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<SongDto>> Create(
            [FromBody] SongCreateRequestDto request,
            CancellationToken cancellationToken)
        {
            var song = await _songService.CreateAsync(request, cancellationToken);

            return Created($"/api/songs/{song.Id}", song);
        }
    }
}
