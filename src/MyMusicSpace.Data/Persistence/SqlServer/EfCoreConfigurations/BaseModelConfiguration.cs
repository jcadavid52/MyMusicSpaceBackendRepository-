using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Persistence.SqlServer.EfCoreConfigurations
{
    internal static class BaseModelConfiguration
    {
        public static void Configure<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : BaseModel
        {
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.CreationDate).HasColumnType("datetime2");
        }
    }
}
