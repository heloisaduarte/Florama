using Florama.Domain.Entities;

namespace Florama.Application.Interfaces.Security
{
    public interface IJwtService
    {
        string GerarToken(Usuario usuario);

        string GerarRefreshToken();
    }
}
