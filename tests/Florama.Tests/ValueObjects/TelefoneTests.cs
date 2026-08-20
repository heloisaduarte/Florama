using Florama.Domain.DomainObjects;
using Florama.Domain.ValueObjects;
using Xunit;

namespace Florama.Tests.Domain.ValueObjects
{
    public class TelefoneTests
    {
        [Theory]
        [InlineData("(11) 98765-4321")]
        [InlineData("11987654321")]
        [InlineData("1133334444")]
        public void Telefone_Valido_E_Criado(string numero)
        {
            var telefone = new Telefone(numero);

            Assert.False(string.IsNullOrEmpty(telefone.Numero));
        }

        [Theory]
        [InlineData("12345")]
        [InlineData("123456789012")]
        [InlineData("")]
        public void Telefone_Invalido_Lanca_DomainException(string numero)
        {
            Assert.Throws<DomainException>(() => new Telefone(numero));
        }

        [Fact]
        public void Telefone_Iguais_Por_Valor()
        {
            var telefone1 = new Telefone("(11) 98765-4321");
            var telefone2 = new Telefone("11987654321");

            Assert.True(telefone1.Equals(telefone2));
        }
    }
}