namespace Florama.Api.Request.Produto
{
    public sealed record ReordenarFotosRequest(List<ReordenarFotoItemRequest> Fotos);

    public sealed record ReordenarFotoItemRequest(long Id, int Ordem);
}