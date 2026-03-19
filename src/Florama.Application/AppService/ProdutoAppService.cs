using AutoMapper;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Notifications;
using FluentValidation;

namespace Florama.Application.AppService
{
    public class ProdutoAppService : BaseAppService, IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IValidator<Produto> _produtoValidator;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ProdutoAppService(
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications,
            IMapper mapper,
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            IValidator<Produto> produtoValidator) : base(uow, notifications)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _produtoValidator = produtoValidator;
        }

        public async Task<ProdutoDto> Atualizar(ProdutoDto dto)
        {
            var produto = await _produtoRepository.ObterPorId(dto.Id);

            if(produto is null)
            {
                NotificarErro("Produto não encontrado.");
                return null;
            }

            var categoria = await _categoriaRepository.GetByIdAsync(dto.CategoriaId);

            if (categoria is null)
            {
                NotificarErro("Categoria não encontrada.");
                return null;
            }

            produto.DefinirValor(dto.Valor);
            produto.DefinirNome(dto.Nome);
            produto.DefinirCategoria(categoria);

            if (!Validar(_produtoValidator, produto))
                return null;

            await _produtoRepository.Update(produto);

            await CommitAsync();

            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task AdicionarFoto(long produtoId, string url)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto is null) 
            {
                NotificarErro("Produto não encontrado.");
                return;
            }

            produto.AdicionarFoto(url);

            await _produtoRepository.Update(produto);

            await CommitAsync();
        }

        public async Task RemoverFoto(long produtoId, long fotoId)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto is null)
            {
                NotificarErro("Produto não encontrado.");
                return;
            }

            try
            {
                produto.RemoverFoto(fotoId);
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return;
            }

            await _produtoRepository.Update(produto);

            await CommitAsync();
        }

        public async Task ReordenarFotos(long produtoId, Dictionary<long, int> novasOrdens)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto is null)
            {
                NotificarErro("Produto não encontrado.");
                return;
            }

            try
            {
                produto.ReordenarFotos(novasOrdens);
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return;
            }

            await _produtoRepository.Update(produto);

            await CommitAsync();
        }
        
        public async Task<ProdutoDto> Criar(ProdutoDto dto)
        {
            var categoria = await _categoriaRepository.GetByIdAsync(dto.CategoriaId);

            if (categoria is null)
            {
                NotificarErro("Categoria não encontrada.");
                return null;
            }

            var produto = new Produto();

            produto.DefinirValor(dto.Valor);
            produto.DefinirNome(dto.Nome);
            produto.DefinirCategoria(categoria);


            foreach (var foto in dto.Fotos)
            {
                produto.AdicionarFoto(foto.Url );
            }

            if (!Validar(_produtoValidator, produto))
                return null;

            await _produtoRepository.Add(produto);

            await CommitAsync();

            return _mapper.Map<ProdutoDto>(produto);
        }
        public async Task<ProdutoDto> ObterPorId(long id)
        {
            var produto = await _produtoRepository.ObterPorId(id);

            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodos();

            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }
    }
}
