namespace MyMusicSpace.Data.Persistence.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Create(TEntity entity, CancellationToken cancellationToken = default);
    }
}
