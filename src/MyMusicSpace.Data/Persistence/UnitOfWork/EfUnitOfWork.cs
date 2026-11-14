using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MyMusicSpace.Data.Exceptions;
using MyMusicSpace.Data.Persistence.SqlServer;

namespace MyMusicSpace.Data.Persistence.UnitOfWork
{
    public sealed class EfUnitOfWork : IUnitOfWork
    {
        // SQL Server incluye el nombre del índice en el mensaje: "... with unique index 'IX_...'".
        // El prefijo del índice no depende del idioma del servidor.
        private static readonly Regex UniqueIndexNameRegex = new(
            @"(?:IX_|UQ_|PK_)[A-Za-z0-9_]+",
            RegexOptions.Compiled);

        private readonly SpaceMusicSqlServerDbContext _context;

        public EfUnitOfWork(SpaceMusicSqlServerDbContext context) => _context = context;

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException { Number: 2601 or 2627 } sqlException)
            {
                var match = UniqueIndexNameRegex.Match(sqlException.Message);

                throw new UniqueConstraintViolationException(
                    match.Success ? match.Value : null,
                    sqlException.Message,
                    ex);
            }
        }

        public void ClearChanges() => _context.ChangeTracker.Clear();
    }
}
