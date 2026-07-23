using Florama.Domain.Abstractions;
using Florama.Domain.Entities;

namespace Florama.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository : IAsyncRepository<Usuario>
    {
        Task<Usuario?> ObterPorEmailAsync(string email);

        Task<Usuario?> ObterPorRefreshTokenAsync(string refreshToken);
    }
}
