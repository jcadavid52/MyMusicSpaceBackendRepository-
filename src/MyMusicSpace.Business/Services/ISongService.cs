using MyMusicSpace.Business.Services.Dtos;

namespace MyMusicSpace.Business.Services
{
    public interface ISongService 
    {
        Task<SongDto> CreateAsync(SongCreateRequestDto request);
    }
}
