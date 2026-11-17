using System.Text.Json.Serialization;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos
{
    public record SpotifyTokenResponseDto(
        [property: JsonPropertyName("access_token")] string AccessToken,
        [property: JsonPropertyName("token_type")] string TokenType,
        [property: JsonPropertyName("expires_in")] int ExpiresIn);
}
