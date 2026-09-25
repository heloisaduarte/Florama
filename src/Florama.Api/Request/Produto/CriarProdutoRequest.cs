using Florama.Api.Request.Estoque;

namespace Florama.Api.Request.Produto
{
    public sealed record CriarProdutoRequest(string Nome, string Descricao, decimal Valor, long CategoriaId, List<AdicionarFotoRequest> Fotos, EstoqueProdutoRequest Estoque);
}