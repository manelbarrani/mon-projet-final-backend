using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Npgsql.Bulk;

namespace Persistance.Data
{
    /// <summary>
    /// Implémentation de base du pattern unit of work
    /// </summary>
    /// <typeparam name="TContext">Type du context</typeparam>
    public class UnitOfWork<TContext> : Domain.Interfaces.IUnitOfWork where TContext : IContext
    {
        /// <summary>
        /// Obtient l'identifiant du context
        /// </summary>
        public Guid? ContextId
        {
            get
            {
                return _contextId ??= Guid.NewGuid();
            }
        }
        private Guid? _contextId;

        /// <summary>
        /// Obtient le current context de type <see cref="IContext"/>
        /// </summary>
        public IContext Context => (TContext)_context;
        private readonly IContext _context;

        /// <summary>
        /// Constructeur avec un context à initialiser
        /// </summary>
        /// <param name="context">Context à initialiser</param>
        public UnitOfWork(IContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Commit tous les changements du context courant
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Bulk insert.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entities">The entities.</param>
        public async Task BulkInsertAsync<T>(IEnumerable<T> entities) where T : class
        {
            if (Context is not DbContext dbContext)
            {
                throw new InvalidOperationException("Le contexte doit être un DbContext pour utiliser BulkInsert.");
            }

            var uploader = new NpgsqlBulkUploader(dbContext);
            await uploader.InsertAsync(entities);
        }

        /// <summary>
        /// Nettoyage des ressources utilisées
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Nettoyage des ressources utilisées
        /// </summary>
        /// <param name="disposing">True permet de libérer toutes les ressources utilisées</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Le contexte est géré par l'injection de dépendances, donc pas besoin de le disposer ici.
            }
        }
    }
}
