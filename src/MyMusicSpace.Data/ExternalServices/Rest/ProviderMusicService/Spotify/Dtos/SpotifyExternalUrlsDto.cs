using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyExternalUrlsDto(
        [property: JsonPropertyName("spotify")] string Spotify);
}
