using AutoMapper;
using Florama.Api.Request.Produto;
using Florama.Api.ViewModel;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Controllers
{

    [Route("api/[controller]")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoAppService _appService;
        private readonly IMapper _mapper;

        public ProdutoController(
            IHandler<DomainNotification> notifications,
            IMapper mapper, 
            IProdutoAppService appService) : base(notifications)
        {
            _mapper = mapper;
            _appService = appService;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar(CriarProdutoRequest request)
        {
            var dto = _mapper.Map<ProdutoDto>(request);

            var result = await _appService.Criar(dto);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(AtualizarProdutoRequest request)
        {
            var dto = _mapper.Map<ProdutoDto>(request);

            var result = await _appService.Atualizar(dto);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        [HttpGet("obter-todos")]
        public async Task<IActionResult> ObterTodos()
        {
            var result = await _appService.ObterTodos();

            return CustomResponse(_mapper.Map<IEnumerable<ProdutoViewModel>>(result));
        }

        [HttpGet("obter-por-id/{id}")]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var result = await _appService.ObterPorId(id);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }
    }
}
