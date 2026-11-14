using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Repositories
{
    public interface IAlbumRepository : IRepository<Album>
    {
        Task<Album?> GetByProviderIdAsync(
            string userName,
            string providerId,
            CancellationToken cancellationToken = default);
    }
}
