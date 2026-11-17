using System.ComponentModel.DataAnnotations;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos
{
    public record ProviderMusicSearchQueryDto(
        [property: Required, StringLength(200, MinimumLength = 1)] string Search,
        [property: Range(1, 50)] int Limit);
}
