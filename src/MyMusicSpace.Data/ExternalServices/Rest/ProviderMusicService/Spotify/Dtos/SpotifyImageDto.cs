using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyImageDto(
          [property: JsonPropertyName("url")] string Url,
          [property: JsonPropertyName("height")] int? Height,
          [property: JsonPropertyName("width")] int? Width);
}
