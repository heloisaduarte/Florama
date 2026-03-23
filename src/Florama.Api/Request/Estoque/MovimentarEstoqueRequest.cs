using Florama.Domain.Enums;

namespace Florama.Api.Request.Estoque
{
    public sealed record MovimentarEstoqueRequest(long ProdutoId, int Quantidade, TipoMovimentacao Tipo);
}
