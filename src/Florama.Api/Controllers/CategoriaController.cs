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
    /// <summary>
    /// Gerenciamento das categorias de produtos.
    /// </summary>
    /// <remarks>
    /// Endpoints de consulta (obter-todos, obter-por-id) são **públicos** — não requerem autenticação.
    /// Endpoints de escrita (criar, atualizar) exigem autenticação com perfil **Admin**.
    /// </remarks>
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

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**.</remarks>
        /// <param name="request">Nome e descrição da categoria.</param>
        /// <returns>Categoria criada com seu id gerado.</returns>
        [HttpPost("criar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Criar(CriarCategoriaRequest request)
        {
            var dto = _mapper.Map<CategoriaDto>(request);

            var result = await _appService.Criar(dto);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <remarks>Requer autenticação e perfil **Admin**. O id informado deve pertencer a uma categoria existente.</remarks>
        /// <param name="request">Id da categoria e novos valores de nome e descrição.</param>
        /// <returns>Categoria atualizada.</returns>
        [HttpPut("atualizar")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Atualizar(AtualizarCategoriaRequest request)
        {
            var dto = _mapper.Map<CategoriaDto>(request);

            var result = await _appService.Atualizar(dto);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }

        /// <summary>
        /// Lista todas as categorias cadastradas.
        /// </summary>
        /// <remarks>Endpoint público — não requer autenticação.</remarks>
        /// <returns>Coleção de categorias.</returns>
        [HttpGet("obter-todos")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(IEnumerable<CategoriaViewModel>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodos()
        {
            var result = await _appService.ObterTodos();

            return CustomResponse(_mapper.Map<IEnumerable<CategoriaViewModel>>(result));
        }

        /// <summary>
        /// Busca uma categoria pelo id.
        /// </summary>
        /// <remarks>Endpoint público — não requer autenticação. Retorna erro 400 se o id não existir.</remarks>
        /// <param name="id">Identificador único da categoria.</param>
        /// <returns>Categoria correspondente ao id informado.</returns>
        [HttpGet("obter-por-id/{id}")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(CategoriaViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ObterPorId(long id)
        {
            var result = await _appService.ObterPorId(id);

            return CustomResponse(_mapper.Map<CategoriaViewModel>(result));
        }
    }
}