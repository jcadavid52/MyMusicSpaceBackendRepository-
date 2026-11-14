using Microsoft.EntityFrameworkCore;
using MyMusicSpace.Data.Models;
using MyMusicSpace.Data.Persistence.SqlServer;

namespace MyMusicSpace.Data.Repositories
{
    public class ArtistSqlServerRepository : IArtistRepository
    {
        private readonly SpaceMusicSqlServerDbContext _context;
        public ArtistSqlServerRepository(SpaceMusicSqlServerDbContext context) => _context = context;

        public Artist Create(Artist artist, CancellationToken cancellationToken = default)
        {
            _context.Artists.Add(artist);

            return artist;
        }

        public async Task<IReadOnlyList<Artist>> GetByProviderIdsAsync(
            string userName,
            IEnumerable<string> providerIds,
            CancellationToken cancellationToken = default)
        {
            var ids = providerIds.ToList();
            if (ids.Count == 0)
            {
                return [];
            }

            return await _context.Artists
                .AsNoTracking()
                .Where(artist => artist.UserName == userName && ids.Contains(artist.ProviderId))
                .ToListAsync(cancellationToken);
        }
    }
}
