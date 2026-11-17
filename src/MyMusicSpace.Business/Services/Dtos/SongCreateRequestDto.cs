using System.ComponentModel.DataAnnotations;

namespace MyMusicSpace.Business.Services.Dtos
{
    public record SongCreateRequestDto(
        [Required, StringLength(200, MinimumLength = 1)] string Name,
        [Required(AllowEmptyStrings = true)] string Lyrics,
        [Required, MinLength(1)] IEnumerable<ArtistCreateRequestDto> Artists,
        [Required] AlbumCreateRequestDto Album,
        [Required, StringLength(100, MinimumLength = 1)] string ProviderId,
        [Required, StringLength(500, MinimumLength = 1)] string ProviderUrlTrack,
        [Range(typeof(long), "1", "9223372036854775807")] long Duration,
        string? Translation);
}
