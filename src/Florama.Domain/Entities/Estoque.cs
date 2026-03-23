using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;

public class Estoque : AuditEntity
{
    public long ProdutoId { get; private set; }
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }

    public Estoque() { }

    public void DefinirProduto(Produto produto)
    {
        if (produto is null)
            throw new DomainException("Produto é obrigatório.");

        Produto = produto;
        ProdutoId = produto.Id;
    }

    public void Adicionar(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");

        Quantidade += quantidade;
    }

    public void Remover(int quantidade)
    {
        if (quantidade <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");

        if (Quantidade < quantidade)
            throw new DomainException("Estoque insuficiente.");

        Quantidade -= quantidade;
    }
}