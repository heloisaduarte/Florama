using Florama.CrossCutting.Extensions;
using Florama.Domain.DomainObjects;
using Florama.Domain.ValueObjects;

namespace Florama.Domain.Entities
{
    public class Usuario : AuditEntity
    {
        public string Nome { get; private set; } = null!;
        public Email Email { get; private set; } = null!;

        public bool IsAdmin { get; private set; }

        public string SenhaHash { get; private set; } = null!;

        public bool Ativo { get; private set; }

        public bool EmailConfirmado { get; private set; }

        public string? RefreshToken { get; private set; }

        public DateTime? RefreshTokenExpiracao { get; private set; }

        public DateTime? UltimoLogin { get; private set; }

        public int TentativasLoginFalhas { get; private set; }

        public DateTime? BloqueioAte { get; private set; }

        protected Usuario() { }

        public Usuario(
            string nome,
            string email,
            bool isAdmin,
            string senhaHash)
        {
            DefinirNome(nome);
            DefinirEmail(email);

            IsAdmin = isAdmin;
            SenhaHash = senhaHash;

            Ativo = true;
            EmailConfirmado = false;
        }

        public void AlterarSenha(string senhaHash)
        {
            if (string.IsNullOrWhiteSpace(senhaHash))
                throw new DomainException("Senha inválida.");

            SenhaHash = senhaHash;
        }

        public void AtualizarRefreshToken(string refreshToken, DateTime expiracao)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiracao = expiracao;
        }

        public void RemoverRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiracao = null;
        }

        public void RegistrarLogin()
        {
            UltimoLogin = DateTime.UtcNow;
        }

        public bool EstaBloqueado()
        {
            return BloqueioAte.HasValue && BloqueioAte.Value > DateTime.UtcNow;
        }

        public void RegistrarFalhaLogin(int maximoTentativas, TimeSpan duracaoBloqueio)
        {
            TentativasLoginFalhas++;

            if (TentativasLoginFalhas >= maximoTentativas)
            {
                BloqueioAte = DateTime.UtcNow.Add(duracaoBloqueio);
                TentativasLoginFalhas = 0;
            }
        }

        public void ResetarFalhasLogin()
        {
            TentativasLoginFalhas = 0;
            BloqueioAte = null;
        }

        public void ConfirmarEmail()
        {
            EmailConfirmado = true;
        }

        public void Ativar()
        {
            Ativo = true;
        }

        public void Desativar()
        {
            Ativo = false;
        }

        public bool PodeCadastrar()
        {
            return IsAdmin;
        }

        public void DefinirEmail(string email)
        {
            Email = new Email(email);
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome obrigatório.");

            if (nome.Length < 3)
                throw new DomainException("Nome muito curto.");

            if (nome.Length > 100)
                throw new DomainException("Nome muito longo.");

            Nome = nome.ToTitleCasePtBr();
        }
    }
}
