using Florama.Application.DTOs;

namespace Florama.Application.Interfaces
{
    public interface ICategoriaAppService : IAppService
    {
        Task<CategoriaDto?> Criar(CategoriaDto dto);
        Task<CategoriaDto?> Atualizar(CategoriaDto dto);
        Task<IEnumerable<CategoriaDto>> ObterTodos();
        Task<CategoriaDto?> ObterPorId(long id);
    }
}
