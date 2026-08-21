using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Xunit;

namespace Florama.Tests.Domain.Entities
{
    public class UsuarioTests
    {
        private static Usuario CriarUsuario()
        {
            return new Usuario(
                nome: "Maria Silva",
                email: "maria@florama.com.br",
                isAdmin: false,
                senhaHash: "hash-teste");
        }

        [Fact]
        public void Criar_Usuario_Valido()
        {
            var usuario = CriarUsuario();

            Assert.True(usuario.Ativo);
            Assert.False(usuario.IsAdmin);
            Assert.False(usuario.EmailConfirmado);
        }

        [Fact]
        public void Nome_Invalido_Lanca_DomainException()
        {
            var usuario = CriarUsuario();

            Assert.Throws<DomainException>(() => usuario.DefinirNome(""));
            Assert.Throws<DomainException>(() => usuario.DefinirNome("Ab"));
        }

        [Fact]
        public void Atualizar_RefreshToken()
        {
            var usuario = CriarUsuario();

            usuario.AtualizarRefreshToken("token", DateTime.UtcNow.AddMinutes(10));

            Assert.Equal("token", usuario.RefreshToken);
            Assert.NotNull(usuario.RefreshTokenExpiracao);
        }

        [Fact]
        public void Remover_RefreshToken()
        {
            var usuario = CriarUsuario();
            usuario.AtualizarRefreshToken("token", DateTime.UtcNow.AddMinutes(10));

            usuario.RemoverRefreshToken();

            Assert.Null(usuario.RefreshToken);
            Assert.Null(usuario.RefreshTokenExpiracao);
        }

        [Fact]
        public void Bloqueio_Apos_Tentativas_Falhas()
        {
            var usuario = CriarUsuario();

            for (int i = 0; i < 5; i++)
            {
                usuario.RegistrarFalhaLogin(maximoTentativas: 5, duracaoBloqueio: TimeSpan.FromMinutes(15));
            }

            Assert.True(usuario.EstaBloqueado());
        }

        [Fact]
        public void Resetar_Falhas_Desbloqueia()
        {
            var usuario = CriarUsuario();

            for (int i = 0; i < 5; i++)
            {
                usuario.RegistrarFalhaLogin(maximoTentativas: 5, duracaoBloqueio: TimeSpan.FromMinutes(15));
            }

            usuario.ResetarFalhasLogin();

            Assert.False(usuario.EstaBloqueado());
            Assert.Equal(0, usuario.TentativasLoginFalhas);
        }

        [Fact]
        public void Confirmar_Email()
        {
            var usuario = CriarUsuario();

            usuario.ConfirmarEmail();

            Assert.True(usuario.EmailConfirmado);
        }
    }
}