using AutoMapper;
using Florama.Api.Request.Produto;
using Florama.Api.ViewModel;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.Notifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florama.Api.Controllers
{
    /// <summary>
    /// Gerenciamento de produtos e suas fotos.
    /// </summary>
    /// <remarks>
    /// Endpoints de consulta (obter-todos, obter-por-id, em-estoque) são **públicos** — não requerem autenticação.
    /// Endpoints de escrita (criar, atualizar, adicionar-foto, ordenar-fotos, remover-foto) exigem autenticação com perfil **Admin**.
    /// </remarks>
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

        /// <summary>
        /// Cria um novo produto com fotos e estoque inicial.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**. A categoria informada (CategoriaId) deve existir.</remarks>
        /// <param name="request">Nome, valor, categoria, lista de fotos (urls) e dados do estoque inicial.</param>
        /// <returns>Produto criado com id, fotos e estoque.</returns>
        [HttpPost("criar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Criar(CriarProdutoRequest request)
        {
            var dto = _mapper.Map<ProdutoDto>(request);

            var result = await _appService.Criar(dto);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        /// <summary>
        /// Adiciona uma nova foto a um produto existente.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**.</remarks>
        /// <param name="produtoId">Id do produto que receberá a foto.</param>
        /// <param name="request">Url da imagem.</param>
        /// <returns>Sucesso (200) sem conteúdo.</returns>
        [HttpPost("adicionar-foto/{produtoId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> AdicionarFoto(long produtoId, AdicionarFotoRequest request)
        {
            await _appService.AdicionarFoto(produtoId, request.Url);

            return CustomResponse();
        }

        /// <summary>
        /// Define a ordem de exibição das fotos de um produto.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**. Envie a lista completa das fotos com o id e a nova ordem desejada para cada uma.</remarks>
        /// <param name="produtoId">Id do produto cujas fotos serão reordenadas.</param>
        /// <param name="request">Lista com id da foto e nova ordem.</param>
        /// <returns>Sucesso (200) sem conteúdo.</returns>
        [HttpPut("ordenar-fotos/{produtoId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Reordenar(long produtoId, ReordenarFotosRequest request)
        {
            var dict = request.Fotos.ToDictionary(x => x.Id, x => x.Ordem);

            await _appService.ReordenarFotos(produtoId, dict);

            return CustomResponse();
        }

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**.</remarks>
        /// <param name="request">Id do produto, nome, valor, categoria e lista de fotos.</param>
        /// <returns>Produto atualizado.</returns>
        [HttpPut("atualizar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Atualizar(AtualizarProdutoRequest request)
        {
            var dto = _mapper.Map<ProdutoDto>(request);

            var result = await _appService.Atualizar(dto);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        /// <summary>
        /// Remove uma foto de um produto.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**.</remarks>
        /// <param name="produtoId">Id do produto dono da foto.</param>
        /// <param name="fotoId">Id da foto a ser removida.</param>
        /// <returns>Sucesso (200) sem conteúdo.</returns>
        [HttpDelete("remover-foto/{produtoId}/{fotoId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> RemoverFoto(long produtoId, long fotoId)
        {
            await _appService.RemoverFoto(produtoId, fotoId);

            return CustomResponse();
        }

        /// <summary>
        /// Lista todos os produtos cadastrados.
        /// </summary>
        /// <remarks>Endpoint público — não requer autenticação.</remarks>
        /// <returns>Coleção de produtos com fotos e estoque.</returns>
        [HttpGet("obter-todos")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ProdutoViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos()
        {
            var result = await _appService.ObterTodos();

            return CustomResponse(_mapper.Map<IEnumerable<ProdutoViewModel>>(result));
        }

        /// <summary>
        /// Busca um produto pelo id.
        /// </summary>
        /// <remarks>Endpoint público — não requer autenticação. Retorna erro 400 se o id não existir.</remarks>
        /// <param name="id">Identificador único do produto.</param>
        /// <returns>Produto correspondente ao id informado.</returns>
        [HttpGet("obter-por-id/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(ProdutoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var result = await _appService.ObterPorId(id);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        /// <summary>
        /// Lista somente os produtos disponíveis em estoque.
        /// </summary>
        /// <remarks>Endpoint público — não requer autenticação. Ideal para vitrine/catálogo.</remarks>
        /// <returns>Coleção de produtos com quantidade em estoque maior que zero.</returns>
        [HttpGet("em-estoque")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<ProdutoViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterEmEstoque()
        {
            var result = await _appService.ObterEmEstoque();

            return CustomResponse(_mapper.Map<IEnumerable<ProdutoViewModel>>(result));
        }
    }
}