namespace Florama.Application.DTOs
{
    public class ProdutoDto
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public string Descricao { get; set; } = null!;
        public decimal Valor { get; set; }
        public long CategoriaId { get; set; }
        public List<ProdutoFotoDto> Fotos { get; set; } = new();
        public EstoqueDto Estoque { get; set; } = new();

    }
}
