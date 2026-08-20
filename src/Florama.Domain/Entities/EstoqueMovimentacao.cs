using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Enums;

public class EstoqueMovimentacao : AuditEntity
{
    public long ProdutoId { get; private set; }
    public Produto Produto { get; private set; } = null!;
    public TipoMovimentacao Tipo { get; private set; }
    public int Quantidade { get; private set; }

    public EstoqueMovimentacao() { }

    public EstoqueMovimentacao(long produtoId, TipoMovimentacao tipo, int quantidade)
    {
        if (produtoId <= 0)
            throw new DomainException("Produto inválido.");

        if (quantidade <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");

        ProdutoId = produtoId;
        Tipo = tipo;
        Quantidade = quantidade;
    }
}