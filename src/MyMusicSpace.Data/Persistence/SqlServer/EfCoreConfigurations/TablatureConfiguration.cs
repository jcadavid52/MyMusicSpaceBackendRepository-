using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations
{
    public class TablatureConfiguration : IEntityTypeConfiguration<Tablature>
    {
        public void Configure(EntityTypeBuilder<Tablature> builder)
        {
            builder.ToTable("Tablatures");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TextChord).HasColumnType("nvarchar(max)");

            builder.Property(x => x.ImagesChords)
                .HasConversion(new ValueConverter<IEnumerable<string>, string>(
                    value => JsonSerializer.Serialize(value),
                    value => JsonSerializer.Deserialize<List<string>>(value) ?? new List<string>()))
                .HasColumnType("nvarchar(max)");
        }
    }
}
