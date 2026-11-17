using Microsoft.Extensions.Options;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify
{
    public class SpotifyGetAccessTokenService : IProviderMusicGetAccessTokenService
    {
        private readonly ProviderMusicOptions _options;
        private readonly HttpClient _httpClient;


        public SpotifyGetAccessTokenService(
            IOptions<ProviderMusicOptions> options,
            HttpClient httpClient)
        {
            _options = options.Value;
            _httpClient = httpClient;
        }

        public Task<string> ExecuteAsync()
        {
            throw new NotImplementedException();
        }
    }
}
