using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations
{
    public class AlbumConfiguration : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("Albums");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(200).IsRequired();
            builder.Property(x => x.ImageUrl).HasMaxLength(500);
            builder.Property(x => x.ProviderId).HasMaxLength(100).IsRequired();
            BaseModelConfiguration.Configure(builder);

            builder.HasIndex(x => new { x.UserName, x.ProviderId })
                .IsUnique()
                .HasDatabaseName(UniqueIndexes.AlbumUserProvider);
        }
    }
}
