using MyMusicSpace.Contracts.Dtos;

namespace MyMusicSpace.Contracts
{
    public interface IProviderMusicSearchService
    {
        Task<SongProviderSearchResponseDto> ExecuteAsync(
            SongProviderSearchQueryDto query,
            CancellationToken cancellationToken = default);
    }
}
