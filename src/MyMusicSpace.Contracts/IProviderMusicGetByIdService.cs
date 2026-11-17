using MyMusicSpace.Contracts.Dtos;

namespace MyMusicSpace.Contracts
{
    public interface IProviderMusicGetByIdService
    {
        Task<SongProviderDto> ExecuteAsync(
            string providerId,
            CancellationToken cancellationToken = default);
    }
}
