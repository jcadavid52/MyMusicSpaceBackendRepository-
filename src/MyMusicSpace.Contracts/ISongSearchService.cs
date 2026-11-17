using MyMusicSpace.Contracts.Dtos;

namespace MyMusicSpace.Contracts
{
    public interface ISongSearchService
    {
        Task<SongSearchResponseDto> ExecuteAsync(
            SongSearchQueryDto query,
            CancellationToken cancellationToken = default);
    }
}
