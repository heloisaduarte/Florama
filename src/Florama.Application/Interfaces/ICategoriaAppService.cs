using Florama.Application.DTOs;

namespace Florama.Application.Interfaces
{
    public interface ICategoriaAppService : IAppService
    {
        Task<CategoriaDto> Criar(CategoriaDto dto);
    }
}
