using MyMusicSpace.Business.Services.Dtos;

namespace MyMusicSpace.Business.Services.Abstractions
{
    public interface ISongSearchService
    {
        Task<SongSearchResponseDto> ExecuteAsync(
            SongSearchQueryDto query,
            CancellationToken cancellationToken = default);
    }
}
