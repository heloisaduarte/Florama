using Florama.Domain.Entities;

namespace Florama.Domain.Interfaces.Security
{
    public interface IJwtService
    {
        string GerarToken(Usuario usuario);

        string GerarRefreshToken();
    }
}