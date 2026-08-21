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
    /// <summary>
    /// Gerenciamento do estoque de produtos.
    /// </summary>
    /// <remarks>
    /// O endpoint de consulta (obter-por-produto) é **público** — não requer autenticação.
    /// O endpoint de escrita (movimentar) exige autenticação com perfil **Admin**.
    /// </remarks>
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

        /// <summary>
        /// Registra uma movimentação de estoque (entrada ou saída).
        /// </summary>
        /// <remarks>
        /// Requer autenticação e perfil **Admin**.
        ///
        /// Tipo de movimentação: `Entrada` (1) soma a quantidade ao estoque; `Saida` (2) subtrai.
        /// A saída será rejeitada caso a quantidade solicitada seja maior que o saldo disponível.
        /// </remarks>
        /// <param name="request">Id do produto, quantidade (> 0) e tipo da movimentação.</param>
        /// <returns>Sucesso (200) sem conteúdo.</returns>
        [HttpPost("movimentar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Movimentar(MovimentarEstoqueRequest request)
        {
            var dto = _mapper.Map<EstoqueMovimentacaoDto>(request);

            await _estoqueAppService.MovimentarEstoque(dto);

            return CustomResponse();
        }

        /// <summary>
        /// Consulta o estoque de um produto.
        /// </summary>
        /// <remarks>Endpoint público — não requer autenticação. Retorna erro 400 se o produto não existir.</remarks>
        /// <param name="produtoId">Id do produto consultado.</param>
        /// <returns>Estoque do produto com id e quantidade atual.</returns>
        [HttpGet("obter-por-produto/{produtoId}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(EstoqueViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorProdutoId(long produtoId)
        {
            var result = await _estoqueAppService.ObterPorProdutoId(produtoId);

            return CustomResponse(_mapper.Map<EstoqueViewModel>(result));
        }
    }
}