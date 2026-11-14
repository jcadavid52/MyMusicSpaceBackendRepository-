using System.ComponentModel.DataAnnotations;

namespace MyMusicSpace.Business.Services.Dtos
{
    public record ArtistCreateRequestDto(
        [Required, StringLength(100, MinimumLength = 1)] string ProviderId,
        [Required, StringLength(200, MinimumLength = 1)] string Name);
}
