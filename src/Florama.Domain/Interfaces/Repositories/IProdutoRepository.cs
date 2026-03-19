using Florama.Domain.Abstractions;
using Florama.Domain.Entities;

namespace Florama.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository : IAsyncRepository<Produto>
    {
    }
}
