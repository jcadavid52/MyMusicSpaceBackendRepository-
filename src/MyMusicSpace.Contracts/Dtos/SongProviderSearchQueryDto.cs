using System.ComponentModel.DataAnnotations;

namespace MyMusicSpace.Contracts.Dtos
{
    public record SongProviderSearchQueryDto(
        [Required, StringLength(200, MinimumLength = 1)] string Search,
        [Range(1, 10)] int Limit = 10);
}
