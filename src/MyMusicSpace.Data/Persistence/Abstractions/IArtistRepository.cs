using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Data.Persistence.Abstractions
{
    public interface IArtistRepository : IRepository<Artist>
    {
        Task<IReadOnlyList<Artist>> GetByProviderIdsAsync(
            string userName,
            IEnumerable<string> providerIds,
            CancellationToken cancellationToken = default);
    }
}
