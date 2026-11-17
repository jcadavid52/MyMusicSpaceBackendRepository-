using Microsoft.EntityFrameworkCore;
using MyMusicSpace.Data.Persistence.Abstractions;
using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.Repositories
{
    public class SongSqlServerRepository : ISongRepository
    {
        private readonly SpaceMusicSqlServerDbContext _context;
        public SongSqlServerRepository(SpaceMusicSqlServerDbContext context) => _context = context;

        public Song Create(Song song, CancellationToken cancellationToken = default)
        {
            _context.Songs.Add(song);

            return song;
        }

        public Task<bool> ExistsAsync(string userName, string providerId, CancellationToken cancellationToken = default) =>
            _context.Songs.AnyAsync(
                song => song.UserName == userName && song.ProviderId == providerId,
                cancellationToken);
    }
}
