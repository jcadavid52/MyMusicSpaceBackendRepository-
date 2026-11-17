using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyErrorDto(
        [property: JsonPropertyName("error")] SpotifyErrorDetailDto Error);

    public record SpotifyErrorDetailDto(
        [property: JsonPropertyName("status")] int Status,
        [property: JsonPropertyName("message")] string Message);
}
