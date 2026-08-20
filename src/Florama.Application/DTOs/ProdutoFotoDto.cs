namespace Florama.Application.DTOs
{
    public class ProdutoFotoDto
    {
        public long Id { get; set; }
        public string Url { get; set; } = null!;
        public int Ordem { get; set; }
    }
}
