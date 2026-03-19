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

        [HttpPost("adicionar-fotos/{produtoId}")]
        public async Task<IActionResult> AdicionarFoto(long produtoId, AdicionarFotoRequest request)
        {
            await _appService.AdicionarFoto(produtoId, request.Url);

            return CustomResponse();
        }

        [HttpPut("ordernar-fotos/{produtoId}")]
        public async Task<IActionResult> Reordenar(long produtoId, ReordenarFotosRequest request)
        {
            var dict = request.Fotos.ToDictionary(x => x.Id, x => x.Ordem);

            await _appService.ReordenarFotos(produtoId, dict);

            return CustomResponse();
        }

        [HttpPut("atualizar")]
        public async Task<IActionResult> Atualizar(AtualizarProdutoRequest request)
        {
            var dto = _mapper.Map<ProdutoDto>(request);

            var result = await _appService.Atualizar(dto);

            return CustomResponse(_mapper.Map<ProdutoViewModel>(result));
        }

        [HttpDelete("remover-fotos/{fotoId}")]
        public async Task<IActionResult> RemoverFoto(long id, long fotoId)
        {
            await _appService.RemoverFoto(id, fotoId);

            return CustomResponse();
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
