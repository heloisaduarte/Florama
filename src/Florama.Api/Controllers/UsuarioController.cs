using AutoMapper;
using Florama.Api.Request.Usuario;
using Florama.Api.ViewModel.Usuario;
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
    /// Cadastro de usuários.
    /// </summary>
    /// <remarks>Endpoint público — não requer autenticação.</remarks>
    [Route("api/[controller]")]
    public class UsuarioController : ApiController
    {
        private readonly IAuthAppService _authAppService;
        private readonly IMapper _mapper;

        public UsuarioController(
            IHandler<DomainNotification> notifications,
            IAuthAppService authAppService,
            IMapper mapper) : base(notifications)
        {
            _authAppService = authAppService;
            _mapper = mapper;
        }

        /// <summary>
        /// Registra um novo usuário na plataforma.
        /// </summary>
        /// <remarks>
        /// Endpoint público — não requer autenticação.
        ///
        /// A senha e a confirmação devem ser idênticas. Novos usuários são criados com perfil comum (não admin).
        /// Após o cadastro, utilize o endpoint de login em **Auth/login** para obter o token.
        /// </remarks>
        /// <param name="request">Dados do novo usuário (nome, email, senha e confirmação de senha).</param>
        /// <returns>Dados do usuário criado (id, nome, email e indicação se é admin).</returns>
        [HttpPost("criar")]
        [AllowAnonymous]
        [EnableRateLimiting("auth")]
        [ProducesResponseType(typeof(UsuarioViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Criar(CriarUsuarioRequest request)
        {
            var dto = _mapper.Map<CriarUsuarioDto>(request);

            var result = await _authAppService.RegistrarUsuarioAsync(dto);

            return CustomResponse(_mapper.Map<UsuarioViewModel>(result));
        }
    }
}
