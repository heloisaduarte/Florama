namespace Florama.Api.Request.Categoria
{
    public sealed record AtualizarCategoriaRequest(long Id, string Nome, string Descricao);
}
