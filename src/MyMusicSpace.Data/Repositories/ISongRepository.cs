using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Repositories
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<bool> ExistsAsync(string userName, string providerId, CancellationToken cancellationToken = default);
    }
}
