using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Spotify;

namespace MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService
{
    public class ProviderMusicOptions
    {
        public const string SectionName = "ProviderMusic";
        public SpotifyOptions Spotify { get; set; } = new();
    }
}
