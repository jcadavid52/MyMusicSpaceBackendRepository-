using Microsoft.AspNetCore.Http;

namespace MyMusicSpace.Business.Services.Dtos
{
    public record TablatureCreateRequestDto(
        IEnumerable<IFormFile> ImageFiles);
}
