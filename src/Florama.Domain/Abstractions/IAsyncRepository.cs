using Florama.Domain.DomainObjects;

namespace Florama.Domain.Abstractions
{
    public interface IAsyncRepository<TEntity> : IRepository where TEntity : Entity
    {
        Task<TEntity> Adicionar(TEntity entity);
        Task<TEntity> Remover(TEntity entity);
        Task<TEntity> Atualizar(TEntity entity);
        Task<TEntity?> ObterPorIdAsync(long id);
        Task<IEnumerable<TEntity>> ObterTodosAsync();
    }
}