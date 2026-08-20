using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Xunit;

namespace Florama.Tests.Domain.Entities
{
    public class CategoriaTests
    {
        [Fact]
        public void Criar_Categoria_Valida()
        {
            var categoria = new Categoria("Sabonetes", "Sabonetes artesanais");

            Assert.Equal("Sabonetes", categoria.Nome);
            Assert.Equal("Sabonetes artesanais", categoria.Descricao);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Ab")]
        public void Nome_Invalido_Lanca_DomainException(string nome)
        {
            Assert.Throws<DomainException>(() => new Categoria(nome, "Descrição válida"));
        }

        [Fact]
        public void Descricao_Invalida_Lanca_DomainException()
        {
            Assert.Throws<DomainException>(() => new Categoria("Nome válido", ""));
        }

        [Fact]
        public void Atualizar_Dados_Validos()
        {
            var categoria = new Categoria("Sabonetes", "Sabonetes artesanais");

            categoria.DefinirNome("Sabonetes Naturais");
            categoria.DefinirDescricao("Sabonetes 100% naturais");

            Assert.Equal("Sabonetes Naturais", categoria.Nome);
            Assert.Equal("Sabonetes 100% naturais", categoria.Descricao);
        }
    }
}