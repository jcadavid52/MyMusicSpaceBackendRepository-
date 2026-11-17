using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifySearchResponseDto(
        [property: JsonPropertyName("tracks")] SpotifyPagingDto<SpotifyTrackDto> Tracks);
}
