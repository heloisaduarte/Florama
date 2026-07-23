using Florama.Application.DTOs.Auth;

namespace Florama.Application.Interfaces
{
    public interface IAuthAppService : IAppService
    {
        Task<LoginDto> LoginAsync(LoginRequestDto request);

        Task<LoginDto> RefreshTokenAsync(RefreshTokenRequestDto request);

        Task LogoutAsync(long usuarioId);

        Task<UsuarioLogadoDto> ObterUsuarioLogadoAsync(long usuarioId);

        Task<UsuarioLogadoDto> RegistrarUsuarioAsync(CriarUsuarioDto dto);
    }
}
