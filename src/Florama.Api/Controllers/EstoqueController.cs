using AutoMapper;
using Florama.Api.Request.Estoque;
using Florama.Api.ViewModel.Estoque;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Controllers
{
    [Route("api/[controller]")]

    public class EstoqueController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly IEstoqueAppService _estoqueAppService;

        public EstoqueController(
            IHandler<DomainNotification> notifications,
            IMapper mapper,
            IEstoqueAppService estoqueAppService) : base(notifications)
        {
            _mapper = mapper;
            _estoqueAppService = estoqueAppService;
        }

        [HttpPost("movimentar")]
        public async Task<IActionResult> Movimentar(MovimentarEstoqueRequest request)
        {
            var dto = _mapper.Map<EstoqueMovimentacaoDto>(request);

            await _estoqueAppService.MovimentarEstoque(dto);

            return CustomResponse();
        }

        [HttpGet("obter-por-produto/{produtoId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorProdutoId(long produtoId)
        {
            var result = await _estoqueAppService.ObterPorProdutoId(produtoId);

            return CustomResponse(_mapper.Map<EstoqueViewModel>(result));
        }
    }
}
