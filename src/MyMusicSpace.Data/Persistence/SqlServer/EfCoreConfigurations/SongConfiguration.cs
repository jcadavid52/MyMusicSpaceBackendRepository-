using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusicSpace.Data.Persistence.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations
{
    public class SongConfiguration : IEntityTypeConfiguration<Song>
    {
        public void Configure(EntityTypeBuilder<Song> builder)
        {
            builder.ToTable("Songs");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.Lyrics).HasColumnType("nvarchar(max)");
            builder.Property(x => x.ProviderId).HasMaxLength(100).IsRequired();
            builder.Property(x => x.ProviderUrlTrack).HasMaxLength(500);
            builder.Property(x => x.Translation).HasColumnType("nvarchar(max)");
            BaseModelConfiguration.Configure(builder);

            builder.HasIndex(x => new { x.UserName, x.ProviderId })
                .IsUnique()
                .HasDatabaseName(UniqueIndexes.SongUserProvider);

            builder.HasOne(x => x.Album)
                .WithMany(x => x.Songs)
                .HasForeignKey(x => x.AlbumId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(x => x.Tablature)
                .WithOne(x => x.Song)
                .HasForeignKey<Tablature>(x => x.SongId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
