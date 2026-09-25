using AutoMapper;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Notifications;

namespace Florama.Application.AppService
{
    public class ProdutoAppService : BaseAppService, IProdutoAppService
    {
        private readonly IMapper _mapper;
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly IEstoqueRepository _estoqueRepository;

        public ProdutoAppService(
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications,
            IMapper mapper,
            IProdutoRepository produtoRepository,
            ICategoriaRepository categoriaRepository,
            IEstoqueRepository estoqueRepository) : base(uow, notifications)
        {
            _mapper = mapper;
            _produtoRepository = produtoRepository;
            _categoriaRepository = categoriaRepository;
            _estoqueRepository = estoqueRepository;
        }

        public async Task<ProdutoDto?> Atualizar(ProdutoDto dto)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(dto.Id);

            if (produto is null)
            {
                NotificarErro("Produto não encontrado.");
                return null;
            }

            var categoria = await _categoriaRepository.ObterPorIdAsync(dto.CategoriaId);

            if (categoria is null)
            {
                NotificarErro("Categoria não encontrada.");
                return null;
            }

            try
            {
                produto.DefinirValor(dto.Valor);
                produto.DefinirNome(dto.Nome);
                produto.DefinirDescricao(dto.Descricao);
                produto.DefinirCategoria(categoria);

                if (dto.Fotos is { Count: > 0 })
                {
                    foreach (var id in produto.Fotos.Select(f => f.Id).ToList())
                    {
                        produto.RemoverFoto(id);
                    }

                    foreach (var foto in dto.Fotos)
                    {
                        produto.AdicionarFoto(foto.Url);
                    }
                }
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return null;
            }

            await _produtoRepository.Atualizar(produto);

            await CommitAsync();

            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task AdicionarFoto(long produtoId, string url)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

            if (produto is null)
            {
                NotificarErro("Produto não encontrado.");
                return;
            }

            try
            {
                produto.AdicionarFoto(url);
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return;
            }

            await _produtoRepository.Atualizar(produto);

            await CommitAsync();
        }

        public async Task RemoverFoto(long produtoId, long fotoId)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

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

            await _produtoRepository.Atualizar(produto);

            await CommitAsync();
        }

        public async Task ReordenarFotos(long produtoId, Dictionary<long, int> novasOrdens)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(produtoId);

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

            await _produtoRepository.Atualizar(produto);

            await CommitAsync();
        }

        public async Task<ProdutoDto?> Criar(ProdutoDto dto)
        {
            var categoria = await _categoriaRepository.ObterPorIdAsync(dto.CategoriaId);

            if (categoria is null)
            {
                NotificarErro("Categoria não encontrada.");
                return null;
            }

            var produto = new Produto();

            try
            {
                produto.DefinirValor(dto.Valor);
                produto.DefinirNome(dto.Nome);
                produto.DefinirDescricao(dto.Descricao);
                produto.DefinirCategoria(categoria);

                foreach (var foto in dto.Fotos)
                {
                    produto.AdicionarFoto(foto.Url);
                }
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
                return null;
            }

            var estoque = new Estoque();
            estoque.DefinirProduto(produto);
            estoque.Adicionar(dto.Estoque.Quantidade);

            await _produtoRepository.Adicionar(produto);
            await _estoqueRepository.Adicionar(estoque);

            await CommitAsync();

            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<ProdutoDto?> ObterPorId(long id)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(id);

            return _mapper.Map<ProdutoDto>(produto);
        }

        public async Task<IEnumerable<ProdutoDto>> ObterTodos()
        {
            var produtos = await _produtoRepository.ObterTodosAsync();

            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }

        public async Task<IEnumerable<ProdutoDto>> ObterEmEstoque()
        {
            var produtos = await _produtoRepository.ObterEmEstoqueAsync();

            return _mapper.Map<IEnumerable<ProdutoDto>>(produtos);
        }
    }
}