using Florama.Application.DTOs;
using Florama.Domain.Enums;

namespace Florama.Application.Interfaces
{
    public interface IEstoqueAppService : IAppService
    {
        Task MovimentarEstoque(EstoqueMovimentacaoDto dto);
        Task<EstoqueDto> ObterPorProdutoId(long id);
    }
}
