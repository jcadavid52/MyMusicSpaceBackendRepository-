using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using MyMusicSpace.Contracts;
using MyMusicSpace.Contracts.Dtos;
using MyMusicSpace.Contracts.Exceptions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    public class SpotifyGetByIdService : IProviderMusicGetByIdService
    {
        private const string TrackPath = "v1/tracks";

        private readonly IProviderMusicAccessTokenService _accessTokenService;
        private readonly HttpClient _httpClient;

        public SpotifyGetByIdService(
            IProviderMusicAccessTokenService accessTokenService,
            HttpClient httpClient)
        {
            _accessTokenService = accessTokenService;
            _httpClient = httpClient;
        }

        public async Task<SongProviderDto> ExecuteAsync(
            string providerId,
            CancellationToken cancellationToken = default)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(providerId);

            var requestUri = $"{TrackPath}/{Uri.EscapeDataString(providerId)}";
            var accessToken = await _accessTokenService.ExecuteAsync(cancellationToken);

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new ProviderMusicSongNotFoundException(providerId);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new ProviderMusicException(
                    $"Spotify track request failed with status code {(int)response.StatusCode}.");
            }

            var track = await response.Content
                .ReadFromJsonAsync<SpotifyTrackDto>(cancellationToken: cancellationToken);

            if (track is null || string.IsNullOrWhiteSpace(track.Id))
            {
                throw new ProviderMusicException("Spotify track response did not contain a track.");
            }

            return SpotifyTrackMapper.ToProviderDto(track);
        }
    }
}
