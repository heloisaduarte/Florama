using AutoMapper;
using Florama.Api.Request.Categoria;
using Florama.Api.ViewModel.Categoria;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriaController : ApiController
    {
        private readonly ICategoriaAppService _appService;
        private readonly IMapper _mapper;

        public CategoriaController(
            ICategoriaAppService appService,
            IHandler<DomainNotification> notifications,
            IMapper mapper
        ) : base(notifications)
        {
            _appService = appService;
            _mapper = mapper;
        }

        [HttpPost("criar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Criar(CriarCategoriaRequest request)
        {
            var dto = _mapper.Map<CategoriaDto>(request);

            var result = await _appService.Criar(dto);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }

        [HttpPut("atualizar")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Atualizar(AtualizarCategoriaRequest request)
        {
            var dto = _mapper.Map<CategoriaDto>(request);

            var result = await _appService.Atualizar(dto);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }

        [HttpGet("obter-todos")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterTodos()
        {
            var result = await _appService.ObterTodos();

            return CustomResponse(_mapper.Map<IEnumerable<CategoriaViewModel>>(result));
        }

        [HttpGet("obter-por-id/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var result = await _appService.ObterPorId(id);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }
    }
}