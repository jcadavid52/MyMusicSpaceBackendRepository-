using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyArtistDto(
         [property: JsonPropertyName("external_urls")] SpotifyExternalUrlsDto ExternalUrls,
         [property: JsonPropertyName("href")] string Href,
         [property: JsonPropertyName("id")] string Id,
         [property: JsonPropertyName("name")] string Name,
         [property: JsonPropertyName("type")] string Type,
         [property: JsonPropertyName("uri")] string Uri);
}
