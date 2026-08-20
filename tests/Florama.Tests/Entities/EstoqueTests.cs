using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Xunit;

namespace Florama.Tests.Domain.Entities
{
    public class EstoqueTests
    {
        [Fact]
        public void Adicionar_Quantidade()
        {
            var estoque = new Estoque();
            estoque.Adicionar(10);

            Assert.Equal(10, estoque.Quantidade);
        }

        [Fact]
        public void Remover_Quantidade()
        {
            var estoque = new Estoque();
            estoque.Adicionar(10);
            estoque.Remover(4);

            Assert.Equal(6, estoque.Quantidade);
        }

        [Fact]
        public void Remover_Mais_Que_Disponivel_Lanca_DomainException()
        {
            var estoque = new Estoque();
            estoque.Adicionar(5);

            Assert.Throws<DomainException>(() => estoque.Remover(6));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void Adicionar_Quantidade_Invalida_Lanca_DomainException(int quantidade)
        {
            var estoque = new Estoque();

            Assert.Throws<DomainException>(() => estoque.Adicionar(quantidade));
        }
    }
}