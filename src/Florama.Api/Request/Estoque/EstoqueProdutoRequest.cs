using Florama.Domain.Enums;

namespace Florama.Api.Request.Estoque
{
    public sealed record EstoqueProdutoRequest(int Quantidade, TipoMovimentacao Tipo);
}
