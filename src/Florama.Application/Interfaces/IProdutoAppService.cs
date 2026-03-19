using Florama.Application.DTOs;

namespace Florama.Application.Interfaces
{
    public interface IProdutoAppService : IAppService
    {
        Task<ProdutoDto> Criar(ProdutoDto dto);
        Task<ProdutoDto> Atualizar(ProdutoDto dto);
        Task<IEnumerable<ProdutoDto>> ObterTodos();
        Task<ProdutoDto> ObterPorId(long id);
    }
}
