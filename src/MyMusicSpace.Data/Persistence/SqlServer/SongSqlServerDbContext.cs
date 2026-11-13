using Microsoft.EntityFrameworkCore;

namespace MyMusicSpace.Data.Persistence.SqlServer
{
    public class SongSqlServerDbContext : DbContext
    {
        public SongSqlServerDbContext(DbContextOptions<SongSqlServerDbContext> options)
            : base(options)
        {
        }
    }
}
