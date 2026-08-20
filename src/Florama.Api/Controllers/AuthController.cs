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

        [HttpPost("login")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var dto = _mapper.Map<LoginRequestDto>(request);
            var response = await _authAppService.LoginAsync(dto);

            return CustomResponse(_mapper.Map<LoginViewModel>(response));
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest request)
        {
            var dto = _mapper.Map<RefreshTokenRequestDto>(request);
            var response = await _authAppService.RefreshTokenAsync(dto);

            return CustomResponse(_mapper.Map<LoginViewModel>(response));
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authAppService.LogoutAsync(_usuarioLogado.UsuarioId);

            return CustomResponse();
        }

        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            var usuario = await _authAppService.ObterUsuarioLogadoAsync(_usuarioLogado.UsuarioId);

            return CustomResponse(_mapper.Map<UsuarioLogadoResponse>(usuario));
        }
    }
}
