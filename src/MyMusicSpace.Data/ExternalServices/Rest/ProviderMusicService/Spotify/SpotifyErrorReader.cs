using System.Net.Http.Json;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    internal static class SpotifyErrorReader
    {
        public static async Task<string> BuildMessageAsync(
            HttpResponseMessage response,
            string fallbackMessage,
            CancellationToken cancellationToken)
        {
            try
            {
                var error = await response.Content
                    .ReadFromJsonAsync<SpotifyErrorDto>(cancellationToken: cancellationToken);

                if (!string.IsNullOrWhiteSpace(error?.Error?.Message))
                {
                    return $"{fallbackMessage} Spotify said: {error.Error.Message}";
                }
            }
            catch (Exception)
            {
                // El cuerpo no era un error JSON de Spotify; se conserva el mensaje base.
            }

            return $"{fallbackMessage} Status code: {(int)response.StatusCode}.";
        }
    }
}
