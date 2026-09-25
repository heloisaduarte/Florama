namespace Florama.Api.Request.Produto
{
    public sealed record AtualizarProdutoRequest(long Id, string Nome, string Descricao, decimal Valor, long CategoriaId, List<AdicionarFotoRequest> fotos);
}