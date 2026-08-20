using Florama.Domain.Abstractions;
using Florama.Domain.Entities;

namespace Florama.Domain.Interfaces.Repositories
{
    public interface IEstoqueRepository : IAsyncRepository<Estoque>
    {
        Task<Estoque?> ObterPorProdutoIdAsync(long id);
    }
}