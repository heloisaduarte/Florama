using Florama.Application.DTOs;

namespace Florama.Application.Interfaces
{
    public interface IProdutoAppService : IAppService
    {
        Task<ProdutoDto> Criar(ProdutoDto dto);
        Task<ProdutoDto> Atualizar(ProdutoDto dto);
        Task<IEnumerable<ProdutoDto>> ObterTodos();
        Task<IEnumerable<ProdutoDto>> ObterEmEstoque();
        Task<ProdutoDto> ObterPorId(long id);
        Task AdicionarFoto(long produtoId, string url);
        Task RemoverFoto(long produtoId, long fotoId);
        Task ReordenarFotos(long produtoId, Dictionary<long, int> novasOrdens);
    }
}
