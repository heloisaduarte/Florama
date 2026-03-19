namespace Florama.Api.Request.Produto
{
    public sealed record AtualizarProdutoRequest(long Id, string Nome, decimal Valor, long CategoriaId);
}