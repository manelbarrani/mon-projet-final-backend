using Application.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;

namespace Persistance.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : Entity
    {
        public readonly DematContext _context;

        public GenericRepository(DematContext context)
            => _context = context;

        public async Task<List<T>> PostRange(List<T> entity)
        {
            foreach (var item in entity)
                await _context.Set<T>().AddAsync(item);
            // Optionnel : sauvegarde des changements ici ou à l'appelant
            // await _context.SaveChangesAsync();
            return entity;
        }

        public Task<T> Post(T entity)
        {
            _context.Set<T>().Add(entity);
            return Task.FromResult(entity);
        }

        public async Task<List<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task Update(T entity)
        {
            _context.Set<T>().Update(entity);
            return Task.CompletedTask;
        }

        public async Task Delete(Guid id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
            }
        }

        public async Task<bool> SoftDelete(Guid id)
        {
            T? entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
                return false;

            if (entity is ISoftDeleteable softDeletableEntity)
            {
                softDeletableEntity.IsDeleted = true;
                softDeletableEntity.DeletedDate = DateTime.UtcNow;
            }
            else
            {
                return false;
            }

            _context.Entry(entity).State = EntityState.Modified;

            return true;
        }

        public async Task<bool> Exists(Guid id)
        {
            T? output = await _context.Set<T>().FindAsync(id);
            return output != null;
        }

        public async Task SaveChange(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Set<T>()
                .FirstOrDefaultAsync(e => !e.IsDeleted && e.Id == id, cancellationToken);
        }
    }
}
