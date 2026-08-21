using AutoMapper;
using Florama.Application.DTOs.Auth;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Interfaces.Security;
using Florama.Domain.Notifications;
using Florama.Domain.Options;
using Microsoft.Extensions.Options;

namespace Florama.Application.AppService
{
    public class AuthAppService : BaseAppService, IAuthAppService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtService _jwtService;
        private readonly JwtOptions _jwtOptions;
        private readonly LoginOptions _loginOptions;

        public AuthAppService(
            IUsuarioRepository usuarioRepository,
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications,
            IMapper mapper,
            IPasswordHasher passwordHasher,
            IJwtService jwtService,
            IOptions<JwtOptions> jwtOptions,
            IOptions<LoginOptions> loginOptions) : base(uow, notifications)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _passwordHasher = passwordHasher;
            _jwtService = jwtService;
            _jwtOptions = jwtOptions.Value;
            _loginOptions = loginOptions.Value;
        }

        public async Task<LoginDto?> LoginAsync(LoginRequestDto request)
        {
            var usuario = await _usuarioRepository.ObterPorEmailAsync(request.Email);

            if (usuario is null)
            {
                NotificarErro("Credenciais inválidas.");
                return null;
            }

            if (usuario.EstaBloqueado())
            {
                NotificarErro("Conta bloqueada temporariamente. Tente novamente mais tarde.");
                return null;
            }

            if (!_passwordHasher.Verify(request.Senha, usuario.SenhaHash))
            {
                usuario.RegistrarFalhaLogin(_loginOptions.MaximoTentativas, TimeSpan.FromMinutes(_loginOptions.BloqueioMinutos));
                await CommitAsync();
                NotificarErro("Credenciais inválidas.");
                return null;
            }

            if (!usuario.Ativo)
            {
                NotificarErro("Usuário inativo.");
                return null;
            }

            usuario.ResetarFalhasLogin();
            usuario.RegistrarLogin();
            var accessToken = _jwtService.GerarToken(usuario);
            var refreshToken = _jwtService.GerarRefreshToken();
            var expiraEm = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes);

            usuario.AtualizarRefreshToken(refreshToken, expiraEm);

            await CommitAsync();

            return new LoginDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiraEm = expiraEm,
                Usuario = _mapper.Map<UsuarioLogadoDto>(usuario)
            };
        }

        public async Task<LoginDto?> RefreshTokenAsync(RefreshTokenRequestDto request)
        {
            var usuario = await _usuarioRepository.ObterPorRefreshTokenAsync(request.RefreshToken);

            if (usuario is null || usuario.RefreshToken != request.RefreshToken)
            {
                NotificarErro("Refresh token inválido.");
                return null;
            }

            if (usuario.RefreshTokenExpiracao <= DateTime.UtcNow)
            {
                NotificarErro("Refresh token expirado.");
                return null;
            }

            if (!usuario.Ativo)
            {
                NotificarErro("Usuário inativo.");
                return null;
            }

            var accessToken = _jwtService.GerarToken(usuario);
            var refreshToken = _jwtService.GerarRefreshToken();
            var expiraEm = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpirationInMinutes);

            usuario.AtualizarRefreshToken(refreshToken, expiraEm);

            await CommitAsync();

            return new LoginDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiraEm = expiraEm,
                Usuario = _mapper.Map<UsuarioLogadoDto>(usuario)
            };
        }

        public async Task LogoutAsync(long usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

            if (usuario is not null)
            {
                usuario.RemoverRefreshToken();
                await CommitAsync();
            }
        }

        public async Task<UsuarioLogadoDto?> ObterUsuarioLogadoAsync(long usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

            return _mapper.Map<UsuarioLogadoDto>(usuario);
        }

        public async Task<UsuarioLogadoDto?> RegistrarUsuarioAsync(CriarUsuarioDto dto)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorEmailAsync(dto.Email);

            if (usuarioExistente is not null)
            {
                NotificarErro("Email já cadastrado.");
                return null;
            }

            try
            {
                var senhaHash = _passwordHasher.Hash(dto.Senha);

                var usuario = new Usuario(
                    dto.Nome,
                    dto.Email,
                    isAdmin: false,
                    senhaHash);

                await _usuarioRepository.Adicionar(usuario);

                await CommitAsync();

                return _mapper.Map<UsuarioLogadoDto>(usuario);
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return null;
            }
        }
    }
}