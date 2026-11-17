using Microsoft.EntityFrameworkCore;
using MyMusicSpace.Data.Persistence.Abstractions;
using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.Repositories
{
    public class AlbumSqlServerRepository : IAlbumRepository
    {
        private readonly SpaceMusicSqlServerDbContext _context;
        public AlbumSqlServerRepository(SpaceMusicSqlServerDbContext context) => _context = context;

        public Album Create(Album album, CancellationToken cancellationToken = default)
        {
            _context.Albums.Add(album);

            return album;
        }

        public Task<Album?> GetByProviderIdAsync(
            string userName,
            string providerId,
            CancellationToken cancellationToken = default) =>
            _context.Albums
                .AsNoTracking()
                .FirstOrDefaultAsync(
                    album => album.UserName == userName && album.ProviderId == providerId,
                    cancellationToken);
    }
}
