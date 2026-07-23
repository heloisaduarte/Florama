using AutoMapper;
using Florama.Api.Request.Usuario;
using Florama.Api.ViewModel.Usuario;
using Florama.Application.DTOs.Auth;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Controllers
{
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

        [HttpPost("criar")]
        [AllowAnonymous]
        public async Task<IActionResult> Criar(CriarUsuarioRequest request)
        {
            var dto = _mapper.Map<CriarUsuarioDto>(request);

            var result = await _authAppService.RegistrarUsuarioAsync(dto);

            return CustomResponse(_mapper.Map<UsuarioViewModel>(result));
        }
    }
}
