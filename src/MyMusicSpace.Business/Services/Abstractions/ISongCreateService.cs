using MyMusicSpace.Business.Services.Dtos;

namespace MyMusicSpace.Business.Services.Abstractions
{
    public interface ISongCreateService
    {
        Task<SongDto> ExecuteAsync(SongCreateRequestDto request, CancellationToken cancellationToken = default);
    }
}
