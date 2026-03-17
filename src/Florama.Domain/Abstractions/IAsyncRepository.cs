using Florama.Domain.DomainObjects;

namespace Florama.Domain.Abstractions
{
    public interface IAsyncRepository<TEntity> : IRepository where TEntity : Entity
    {
        Task<TEntity> Add(TEntity entity);
        Task<TEntity> Delete(TEntity entity);
        Task<TEntity> Update(TEntity entity);
        Task<TEntity?> GetByIdAsync(long id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
