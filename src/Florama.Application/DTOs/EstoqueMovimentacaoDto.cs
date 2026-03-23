using Florama.Domain.Enums;

namespace Florama.Application.DTOs
{
    public class EstoqueMovimentacaoDto
    {
        public long ProdutoId { get; set; }
        public int Quantidade { get; set; }
        public TipoMovimentacao Tipo {  get; set; }
    }
}
