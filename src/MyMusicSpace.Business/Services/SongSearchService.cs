using MyMusicSpace.Business.Services.Abstractions;
using MyMusicSpace.Business.Services.Dtos;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Abstractions;
using MyMusicSpace.Data.ExternalServices.Rest.ProviderMusicService.Dtos;

namespace MyMusicSpace.Business.Services
{
    public class SongSearchService : ISongSearchService
    {
        private readonly IProviderMusicSearchService _providerMusicSearchService;

        public SongSearchService(IProviderMusicSearchService providerMusicSearchService)
            => _providerMusicSearchService = providerMusicSearchService;

        public async Task<SongSearchResponseDto> ExecuteAsync(
            SongSearchQueryDto query,
            CancellationToken cancellationToken = default)
        {
            var result = await _providerMusicSearchService.ExecuteAsync(
                new ProviderMusicSearchQueryDto(query.Search, query.Limit),
                cancellationToken);

            return new SongSearchResponseDto(
                result.Tracks.Select(MapTrack).ToArray(),
                result.Total);
        }

        private static SongSearchResultDto MapTrack(ProviderMusicTrackDto track) =>
            new(
                track.ProviderId,
                track.Name,
                track.ProviderUrlTrack,
                track.DurationMs,
                new AlbumSearchResultDto(track.Album.ProviderId, track.Album.Name, track.Album.ImageUrl),
                track.Artists
                    .Select(artist => new ArtistSearchResultDto(artist.ProviderId, artist.Name))
                    .ToArray());
    }
}
