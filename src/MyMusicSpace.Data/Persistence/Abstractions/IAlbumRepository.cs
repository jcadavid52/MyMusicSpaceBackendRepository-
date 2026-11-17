using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Data.Persistence.Abstractions
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album?> GetByProviderIdAsync(
            string userName,
            string providerId,
            CancellationToken cancellationToken = default);
    }
}
