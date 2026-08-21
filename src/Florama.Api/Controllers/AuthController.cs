using AutoMapper;
using Florama.Api.Request.Auth;
using Florama.Api.ViewModel.Auth;
using Florama.Application.DTOs.Auth;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Florama.Api.Controllers
{
    /// <summary>
    /// Autenticação e sessão do usuário (login, refresh token, logout e dados do usuário autenticado).
    /// </summary>
    /// <remarks>
    /// Endpoints públicos (não exigem token): **login** e **refresh-token**.
    /// Endpoints autenticados (exigem Bearer Token): **logout** e **me**.
    /// Os endpoints de login e refresh-token possuem rate limiting de 10 requisições por minuto por IP.
    /// </remarks>
    [Route("api/[controller]")]
    public class AuthController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IAuthAppService _authAppService;
        private readonly IUsuarioLogado _usuarioLogado;

        public AuthController(
            IHandler<DomainNotification> notifications,
            IMapper mapper,
            IAuthAppService authAppService,
            IUsuarioLogado usuarioLogado) : base(notifications)
        {
            _mapper = mapper;
            _authAppService = authAppService;
            _usuarioLogado = usuarioLogado;
        }

        /// <summary>
        /// Autentica o usuário com email e senha.
        /// </summary>
        /// <remarks>
        /// Endpoint público — não requer autenticação.
        ///
        /// Em caso de sucesso, retorna o access token JWT, o refresh token, a data/hora de expiração e os dados do usuário.
        /// Use o access token no botão "Authorize" do Swagger para acessar endpoints protegidos.
        /// Limitado a 10 tentativas por minuto por IP.
        /// </remarks>
        /// <param name="request">Credenciais de acesso (email e senha).</param>
        /// <returns>Token de acesso, refresh token e dados do usuário autenticado.</returns>
        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        [ProducesResponseType(typeof(LoginViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var dto = _mapper.Map<LoginRequestDto>(request);
            var response = await _authAppService.LoginAsync(dto);

            return CustomResponse(_mapper.Map<LoginViewModel>(response));
        }

        /// <summary>
        /// Renova o token de acesso usando um refresh token válido.
        /// </summary>
        /// <remarks>
        /// Endpoint público — não requer autenticação.
        ///
        /// Informe um refresh token válido para receber um novo par de access/refresh token.
        /// O refresh token anterior é invalidado após o uso. Limitado a 10 chamadas por minuto por IP.
        /// </remarks>
        /// <param name="request">Refresh token emitido no login.</param>
        /// <returns>Novo token de acesso, novo refresh token e dados do usuário.</returns>
        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        [ProducesResponseType(typeof(LoginViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var dto = _mapper.Map<RefreshTokenRequestDto>(request);
            var response = await _authAppService.RefreshTokenAsync(dto);

            return CustomResponse(_mapper.Map<LoginViewModel>(response));
        }

        /// <summary>
        /// Encerra a sessão do usuário autenticado (invalida o refresh token).
        /// </summary>
        /// <remarks>Requer autenticação (Bearer Token).</remarks>
        /// <returns>Sucesso (200) sem conteúdo.</returns>
        [HttpPost("logout")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Logout()
        {
            await _authAppService.LogoutAsync(_usuarioLogado.UsuarioId);

            return CustomResponse();
        }

        /// <summary>
        /// Retorna os dados do usuário autenticado.
        /// </summary>
        /// <remarks>Requer autenticação (Bearer Token).</remarks>
        /// <returns>Dados (id, nome, email) do usuário dono do token informado.</returns>
        [HttpGet("me")]
        [ProducesResponseType(typeof(UsuarioLogadoResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Me()
        {
            var usuario = await _authAppService.ObterUsuarioLogadoAsync(_usuarioLogado.UsuarioId);

            return CustomResponse(_mapper.Map<UsuarioLogadoResponse>(usuario));
        }
    }
}
