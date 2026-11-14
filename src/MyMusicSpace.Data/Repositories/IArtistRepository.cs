using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Repositories
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<IReadOnlyList<Artist>> GetByProviderIdsAsync(
            string userName,
            IEnumerable<string> providerIds,
            CancellationToken cancellationToken = default);
    }
}
