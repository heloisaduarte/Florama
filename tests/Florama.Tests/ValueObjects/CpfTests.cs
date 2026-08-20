using Florama.Domain.DomainObjects;
using Florama.Domain.ValueObjects;
using Xunit;

namespace Florama.Tests.Domain.ValueObjects
{
    public class CpfTests
    {
        [Theory]
        [InlineData("529.982.247-25")]
        [InlineData("52998224725")]
        [InlineData("111.444.777-35")]
        public void Cpf_Valido_E_Criado(string numero)
        {
            var cpf = new Cpf(numero);

            Assert.Equal("52998224725".Length, cpf.CpfLimpo.Length);
        }

        [Theory]
        [InlineData("111.111.111-11")]
        [InlineData("123.456.789-09")]
        [InlineData("529.982.247-2")]
        [InlineData("")]
        [InlineData("abc")]
        public void Cpf_Invalido_Lanca_DomainException(string numero)
        {
            Assert.Throws<DomainException>(() => new Cpf(numero));
        }

        [Fact]
        public void Cpf_Iguais_Por_Valor()
        {
            var cpf1 = new Cpf("529.982.247-25");
            var cpf2 = new Cpf("52998224725");

            Assert.True(cpf1.Equals(cpf2));
            Assert.Equal(cpf1.GetHashCode(), cpf2.GetHashCode());
        }

        [Fact]
        public void Cpf_Formatado_Retorna_Mascara()
        {
            var cpf = new Cpf("52998224725");

            Assert.Equal("529.982.247-25", cpf.ObterCpfFormatado());
        }
    }
}