using System.Net.Http.Headers;
using System.Net.Http.Json;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Exceptions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    public class SpotifySearchService : IProviderMusicSearchService
    {
        private const string SearchPath = "v1/search";
        private const int DefaultLimit = 10;
        private const int MaxLimit = 50;

        private readonly IProviderMusicGetAccessTokenService _accessTokenService;
        private readonly HttpClient _httpClient;

        public SpotifySearchService(
            IProviderMusicGetAccessTokenService accessTokenService,
            HttpClient httpClient)
        {
            _accessTokenService = accessTokenService;
            _httpClient = httpClient;
        }

        public async Task<ProviderMusicSearchResponseDto> ExecuteAsync(
            ProviderMusicSearchQueryDto query,
            CancellationToken cancellationToken = default)
        {
            ArgumentNullException.ThrowIfNull(query);
            ArgumentException.ThrowIfNullOrWhiteSpace(query.Search);

            var limit = query.Limit <= 0 ? DefaultLimit : Math.Min(query.Limit, MaxLimit);
            var requestUri = $"{SearchPath}?q={Uri.EscapeDataString(query.Search)}&type=track&limit={limit}";

            var accessToken = await _accessTokenService.ExecuteAsync(cancellationToken);

            using var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            using var response = await _httpClient.SendAsync(request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                throw new ProviderMusicException(
                    $"Spotify search failed with status code {(int)response.StatusCode}.");
            }

            var searchResponse = await response.Content
                .ReadFromJsonAsync<SpotifySearchResponseDto>(cancellationToken: cancellationToken);

            if (searchResponse?.Tracks is null)
            {
                throw new ProviderMusicException("Spotify search response did not contain tracks.");
            }

            var tracks = searchResponse.Tracks.Items
                .Where(track => track is not null)
                .Select(MapTrack)
                .ToArray();

            return new ProviderMusicSearchResponseDto(tracks, searchResponse.Tracks.Total);
        }

        private static ProviderMusicTrackDto MapTrack(SpotifyTrackDto track) =>
            new(
                track.Id,
                track.Name,
                track.ExternalUrls.Spotify,
                track.DurationMs,
                new ProviderMusicAlbumDto(
                    track.Album.Id,
                    track.Album.Name,
                    track.Album.Images?.FirstOrDefault()?.Url),
                track.Artists
                    .Select(artist => new ProviderMusicArtistDto(artist.Id, artist.Name))
                    .ToArray());
    }
}
