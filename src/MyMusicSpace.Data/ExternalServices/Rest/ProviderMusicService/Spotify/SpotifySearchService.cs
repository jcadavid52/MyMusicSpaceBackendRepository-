using Microsoft.Extensions.Options;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    public class SpotifySearchService : IProviderMusicSearchService
    {
        private readonly ProviderMusicOptions _options;
        private readonly HttpClient _httpClient;

        public SpotifySearchService(
            IOptions<ProviderMusicOptions> options,
            HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public Task<ProviderMusicSearchResponseDto> ExecuteAsync(ProviderMusicSearchQueryDto query, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
