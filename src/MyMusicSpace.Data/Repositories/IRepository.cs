using MyMusicSpace.Data.Models;

namespace MyMusicSpace.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity, CancellationToken cancellationToken = default);
    }
}
