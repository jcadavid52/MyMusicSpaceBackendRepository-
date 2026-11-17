using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Data.Persistence.Abstractions
{
    public interface ISongRepository : IRepository<Song>
    {
        Task<bool> ExistsAsync(string userName, string providerId, CancellationToken cancellationToken = default);
    }
}
