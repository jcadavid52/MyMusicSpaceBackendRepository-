using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations
{
    public class ArtistSongConfiguration : IEntityTypeConfiguration<ArtistSong>
    {
        public void Configure(EntityTypeBuilder<ArtistSong> builder)
        {
            builder.ToTable("ArtistSongs");
            builder.HasKey(x => new { x.ArtistId, x.SongId });
            BaseModelConfiguration.Configure(builder);

            builder.HasOne(x => x.Song)
                .WithMany(x => x.ArtistSongs)
                .HasForeignKey(x => x.SongId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.Artist)
                .WithMany(x => x.ArtistSongs)
                .HasForeignKey(x => x.ArtistId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
