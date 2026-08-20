using Florama.Domain.Abstractions;
using System.Security.Claims;

namespace Florama.Api.Extensions
{
    public class UsuarioLogado : IUsuarioLogado
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UsuarioLogado(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public long UsuarioId
        {
            get
            {
                var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
                return claim is not null && long.TryParse(claim.Value, out var id) ? id : 0;
            }
        }
    }
}
