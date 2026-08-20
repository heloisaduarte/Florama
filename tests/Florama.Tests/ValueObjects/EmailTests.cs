using Florama.Domain.DomainObjects;
using Florama.Domain.ValueObjects;
using Xunit;

namespace Florama.Tests.Domain.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("usuario@florama.com.br")]
        [InlineData("teste@dominio.com")]
        public void Email_Valido_E_Criado(string endereco)
        {
            var email = new Email(endereco);

            Assert.Equal(endereco, email.Endereco);
        }

        [Theory]
        [InlineData("invalido")]
        [InlineData("invalido@")]
        [InlineData("@dominio.com")]
        [InlineData("")]
        [InlineData(null)]
        public void Email_Invalido_Lanca_DomainException(string? endereco)
        {
            Assert.Throws<DomainException>(() => new Email(endereco!));
        }

        [Fact]
        public void Email_Iguais_Ignorando_Caixa()
        {
            var email1 = new Email("Usuario@Florama.com.br");
            var email2 = new Email("usuario@florama.com.br");

            Assert.True(email1.Equals(email2));
            Assert.Equal(email1.GetHashCode(), email2.GetHashCode());
        }
    }
}