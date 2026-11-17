using System.ComponentModel.DataAnnotations;

namespace MyMusicSpace.Contracts.Dtos
{
    public record SongSearchQueryDto(
        [property: Required, StringLength(200, MinimumLength = 1)] string Search,
        [property: Range(1, 50)] int Limit = 20);
}
