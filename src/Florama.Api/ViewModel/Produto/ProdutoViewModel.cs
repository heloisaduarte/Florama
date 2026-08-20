using Florama.Api.ViewModel.Estoque;
using Florama.Api.ViewModel.Produto;

namespace Florama.Api.ViewModel
{
    public class ProdutoViewModel
    {
        public long Id { get; set; }
        public string Nome { get; set; } = null!;
        public decimal Valor { get; set; }
        public long CategoriaId { get; set; }
        public List<ProdutoFotoViewModel> Fotos { get; set; } = new();
        public EstoqueViewModel Estoque { get; set; } = null!;
    }
}
