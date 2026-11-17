using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyExternalIdsDto(
         [property: JsonPropertyName("isrc")] string? Isrc,
         [property: JsonPropertyName("ean")] string? Ean,
         [property: JsonPropertyName("upc")] string? Upc);
}
