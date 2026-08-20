using Florama.Domain.Abstractions;
using Florama.Domain.Entities;

namespace Florama.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IAsyncRepository<Produto>
    {
        new Task<IEnumerable<Produto>> ObterTodosAsync();
        Task<IEnumerable<Produto>> ObterEmEstoqueAsync();
        new Task<Produto?> ObterPorIdAsync(long id);
    }
}