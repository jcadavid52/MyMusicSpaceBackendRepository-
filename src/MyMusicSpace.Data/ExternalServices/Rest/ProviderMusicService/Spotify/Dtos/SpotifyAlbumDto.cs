using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyAlbumDto(
        [property: JsonPropertyName("album_type")] string AlbumType,
        [property: JsonPropertyName("artists")] SpotifyArtistDto[] Artists,
        [property: JsonPropertyName("external_urls")] SpotifyExternalUrlsDto ExternalUrls,
        [property: JsonPropertyName("href")] string Href,
        [property: JsonPropertyName("id")] string Id,
        [property: JsonPropertyName("images")] SpotifyImageDto[] Images,
        [property: JsonPropertyName("is_playable")] bool? IsPlayable,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("release_date")] string ReleaseDate,
        [property: JsonPropertyName("release_date_precision")] string ReleaseDatePrecision,
        [property: JsonPropertyName("total_tracks")] int TotalTracks,
        [property: JsonPropertyName("type")] string Type,
        [property: JsonPropertyName("uri")] string Uri);
}
