namespace Florama.Api.ViewModel
{
    public class ProdutoViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public long CategoriaId { get; set; }
        public List<ProdutoFotoViewModel> Fotos { get; set; } = new();

    }
}
