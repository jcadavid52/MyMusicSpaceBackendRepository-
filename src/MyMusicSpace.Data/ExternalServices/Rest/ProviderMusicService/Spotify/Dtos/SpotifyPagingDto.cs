using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyPagingDto<T>(
        [property: JsonPropertyName("href")] string Href,
        [property: JsonPropertyName("items")] T[] Items,
        [property: JsonPropertyName("limit")] int Limit,
        [property: JsonPropertyName("next")] string? Next,
        [property: JsonPropertyName("offset")] int Offset,
        [property: JsonPropertyName("previous")] string? Previous,
        [property: JsonPropertyName("total")] int Total);
}
