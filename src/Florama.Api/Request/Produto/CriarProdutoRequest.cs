namespace Florama.Api.Request.Produto
{
    public sealed record CriarProdutoRequest(string Nome, decimal Valor, long CategoriaId, List<AdicionarFotoRequest> Fotos);
}