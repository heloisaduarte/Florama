using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Xunit;

namespace Florama.Tests.Domain.Entities
{
    public class ProdutoTests
    {
        private static Produto CriarProduto()
        {
            var categoria = new Categoria("Velas Aromáticas", "Velas para aromatização de ambientes");
            var produto = new Produto();
            produto.DefinirNome("Vela de Lavanda");
            produto.DefinirValor(25.90m);
            produto.DefinirCategoria(categoria);
            return produto;
        }

        [Fact]
        public void Adicionar_Foto_Define_Ordem_Sequencial()
        {
            var produto = CriarProduto();

            produto.AdicionarFoto("https://img.com/foto1.jpg");
            produto.AdicionarFoto("https://img.com/foto2.jpg");

            Assert.Equal(2, produto.Fotos.Count);
            Assert.Equal(1, produto.Fotos.First(f => f.Url.EndsWith("foto1.jpg")).Ordem);
            Assert.Equal(2, produto.Fotos.First(f => f.Url.EndsWith("foto2.jpg")).Ordem);
        }

        [Fact]
        public void Remover_Foto_Reordena_Automaticamente()
        {
            var produto = CriarProduto();
            produto.AdicionarFoto("https://img.com/foto1.jpg");
            produto.AdicionarFoto("https://img.com/foto2.jpg");
            produto.AdicionarFoto("https://img.com/foto3.jpg");

            var fotoRemovida = produto.Fotos.First(f => f.Ordem == 2);
            produto.RemoverFoto(fotoRemovida.Id);

            Assert.Equal(2, produto.Fotos.Count);
            Assert.All(produto.Fotos, f => Assert.True(f.Ordem > 0));
        }

        [Fact]
        public void Reordenar_Fotos_Com_Ordens_Duplicadas_Lanca_Excecao()
        {
            var produto = CriarProduto();
            produto.AdicionarFoto("https://img.com/foto1.jpg");
            produto.AdicionarFoto("https://img.com/foto2.jpg");

            var fotos = produto.Fotos.ToList();
            AtribuirId(fotos[0], 1);
            AtribuirId(fotos[1], 2);

            var ordens = new Dictionary<long, int> { { 1, 1 }, { 2, 1 } };

            Assert.Throws<DomainException>(() => produto.ReordenarFotos(ordens));
        }

        private static void AtribuirId(ProdutoFoto foto, long id)
        {
            typeof(Entity).GetProperty(nameof(Entity.Id))!.SetValue(foto, id);
        }

        [Fact]
        public void Valor_Invalido_Lanca_DomainException()
        {
            var produto = CriarProduto();

            Assert.Throws<DomainException>(() => produto.DefinirValor(0));
            Assert.Throws<DomainException>(() => produto.DefinirValor(-10));
        }

        [Fact]
        public void Nome_Invalido_Lanca_DomainException()
        {
            var produto = CriarProduto();

            Assert.Throws<DomainException>(() => produto.DefinirNome(""));
            Assert.Throws<DomainException>(() => produto.DefinirNome("Ab"));
        }
    }
}