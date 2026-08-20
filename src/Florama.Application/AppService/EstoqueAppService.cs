using AutoMapper;
using Florama.Application.DTOs;
using Florama.Application.Interfaces;
using Florama.Domain.Abstractions;
using Florama.Domain.DomainObjects;
using Florama.Domain.Enums;
using Florama.Domain.Interfaces.Repositories;
using Florama.Domain.Notifications;

namespace Florama.Application.AppService
{
    public class EstoqueAppService : BaseAppService, IEstoqueAppService
    {
        private readonly IMapper _mapper;
        private readonly IEstoqueRepository _estoqueRepository;
        private readonly IEstoqueMovimentacaoRepository _movRepository;
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueAppService(
            IUnitOfWork uow,
            IHandler<DomainNotification> notifications,
            IEstoqueRepository estoqueRepository,
            IEstoqueMovimentacaoRepository movRepository,
            IProdutoRepository produtoRepository,
            IMapper mapper) : base(uow, notifications)
        {
            _estoqueRepository = estoqueRepository;
            _movRepository = movRepository;
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task MovimentarEstoque(EstoqueMovimentacaoDto dto)
        {
            var produto = await _produtoRepository.ObterPorIdAsync(dto.ProdutoId);

            if (produto is null)
            {
                NotificarErro("Produto não encontrado.");
                return;
            }

            var estoque = await _estoqueRepository.ObterPorProdutoIdAsync(dto.ProdutoId);

            if (estoque is null)
            {
                estoque = new Estoque();
                estoque.DefinirProduto(produto);
            }

            try
            {
                if (dto.Tipo == TipoMovimentacao.Entrada)
                    estoque.Adicionar(dto.Quantidade);
                else
                    estoque.Remover(dto.Quantidade);

                var movimentacao = new EstoqueMovimentacao(dto.ProdutoId, dto.Tipo, dto.Quantidade);

                if (estoque.Id == 0)
                    await _estoqueRepository.Adicionar(estoque);
                else
                    await _estoqueRepository.Atualizar(estoque);

                await _movRepository.Adicionar(movimentacao);

                await CommitAsync();
            }
            catch (DomainException ex)
            {
                NotificarErro(ex.Message);
            }
        }

        public async Task<EstoqueDto> ObterPorProdutoId(long id)
        {
            var estoque = await _estoqueRepository.ObterPorProdutoIdAsync(id);

            return _mapper.Map<EstoqueDto>(estoque);
        }
    }
}