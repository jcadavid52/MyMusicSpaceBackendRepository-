using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyTrackDto(
        [property: JsonPropertyName("album")] SpotifyAlbumDto Album,
        [property: JsonPropertyName("artists")] SpotifyArtistDto[] Artists,
        [property: JsonPropertyName("disc_number")] int DiscNumber,
        [property: JsonPropertyName("duration_ms")] int DurationMs,
        [property: JsonPropertyName("explicit")] bool Explicit,
        [property: JsonPropertyName("external_ids")] SpotifyExternalIdsDto? ExternalIds,
        [property: JsonPropertyName("external_urls")] SpotifyExternalUrlsDto ExternalUrls,
        [property: JsonPropertyName("href")] string Href,
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("is_local")] bool IsLocal,
        [property: JsonPropertyName("is_playable")] bool? IsPlayable,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("preview_url")] string? PreviewUrl,
        [property: JsonPropertyName("track_number")] int TrackNumber,
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("uri")] string Uri);
}
