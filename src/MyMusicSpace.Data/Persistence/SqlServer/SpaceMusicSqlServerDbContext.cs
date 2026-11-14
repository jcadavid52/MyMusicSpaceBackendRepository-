using Microsoft.EntityFrameworkCore;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer
{
    public class SpaceMusicSqlServerDbContext : DbContext
    {
        public DbSet<Song> Songs => Set<Song>();
        public DbSet<Album> Albums => Set<Album>();
        public DbSet<Artist> Artists => Set<Artist>();
        public DbSet<ArtistSong> ArtistSongs => Set<ArtistSong>();
        public DbSet<Tablature> Tablatures => Set<Tablature>();

        public SpaceMusicSqlServerDbContext(DbContextOptions<SpaceMusicSqlServerDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) =>
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SpaceMusicSqlServerDbContext).Assembly);
    }
}
