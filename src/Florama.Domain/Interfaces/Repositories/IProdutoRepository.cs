using Florama.Domain.Abstractions;
using Florama.Domain.Entities;

namespace Florama.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IAsyncRepository<Produto>
    {
        Task<IEnumerable<Produto>> ObterTodos(); 
        Task<IEnumerable<Produto>> ObterEmEstoque(); 
        Task<Produto> ObterPorId(long Id); 
    }
}
