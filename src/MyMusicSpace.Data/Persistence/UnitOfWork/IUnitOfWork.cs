namespace MyMusicSpace.Data.Persistence.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Deja de trackear el grafo de entidades actual. Necesario antes de reintentar
        /// una operación que falló al guardar, porque EF mantiene las entidades como Added.
        /// </summary>
        void ClearChanges();
    }
}
