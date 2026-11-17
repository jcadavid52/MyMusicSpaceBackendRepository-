using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Microsoft.Extensions.Options;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Exceptions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    public class SpotifyGetAccessTokenService : IProviderMusicGetAccessTokenService
    {
        private const string GrantType = "client_credentials";

        private readonly ProviderMusicOptions _options;
        private readonly HttpClient _httpClient;

        public SpotifyGetAccessTokenService(
            IOptions<ProviderMusicOptions> options,
            HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public async Task<string> ExecuteAsync(CancellationToken cancellationToken = default)
        {
            var spotify = _options.Spotify;
            var credentials = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{spotify.ClientId}:{spotify.ClientSecret}"));

            using var request = new HttpRequestMessage(HttpMethod.Post, string.Empty);
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);
            request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = GrantType
            });

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new ProviderMusicAuthenticationException(
                    $"Spotify token request failed with status code {(int)response.StatusCode}.");
            }

            var token = await response.Content
                .ReadFromJsonAsync<SpotifyTokenResponseDto>(cancellationToken: cancellationToken);

            if (token is null || string.IsNullOrWhiteSpace(token.AccessToken))
            {
                throw new ProviderMusicAuthenticationException(
                    "Spotify token response did not contain an access token.");
            }

            return token.AccessToken;
        }
    }
}
