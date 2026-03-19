namespace Florama.Application.DTOs
{
    public class ProdutoDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public long CategoriaId { get; set; }
        public List<ProdutoFotoDto> Fotos { get; set; } = new();

    }
}
