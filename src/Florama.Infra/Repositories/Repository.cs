using Florama.Data;
using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Repositories
{
    public abstract class Repository<TEntity> : IAsyncRepository<TEntity> where TEntity : Entity
    {
        protected readonly FloramaDbContext _db;

        protected Repository(FloramaDbContext db)
        {
            _db = db;
        }

        public virtual async Task<TEntity> Adicionar(TEntity entity)
        {
            await _db.Set<TEntity>().AddAsync(entity);
            return entity;
        }

        public virtual Task<TEntity> Atualizar(TEntity entity)
        {
            _db.Set<TEntity>().Update(entity);
            return Task.FromResult(entity);
        }

        public virtual Task<TEntity> Remover(TEntity entity)
        {
            _db.Set<TEntity>().Remove(entity);
            return Task.FromResult(entity);
        }

        public virtual async Task<TEntity?> ObterPorIdAsync(long id)
        {
            return await _db.Set<TEntity>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodosAsync()
        {
            return await _db.Set<TEntity>().AsNoTracking().ToListAsync();
        }
    }
}