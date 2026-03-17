using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Florama.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Florama.Infra.Repositories
{
    public abstract class Repository<TEntity> : IAsyncRepository<TEntity> where TEntity : Entity
    {
        protected readonly FloramaDbContext _db;

        protected Repository(FloramaDbContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public virtual async Task<TEntity> Add(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual Task<TEntity> Update(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

        public virtual Task<TEntity> Delete(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            return Task.FromResult(entity);
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
        }
    }
}
