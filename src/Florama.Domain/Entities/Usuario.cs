using Florama.CrossCutting.Extensions;
using Florama.Domain.DomainObjects;
using Florama.Domain.ValueObjects;

namespace Florama.Domain.Entities
{
    public class Usuario : AuditEntity
    {
        public string Nome { get; private set; }
        public Email Email { get; private set; }
        public Cpf Cpf { get; private set; }
        public DateOnly DataNascimento { get; private set; }
        public Telefone Telefone { get; private set; }

        public bool IsAdmin { get; private set; }

        public string SenhaHash { get; private set; }

        public bool Ativo { get; private set; }

        public bool EmailConfirmado { get; private set; }

        public string? RefreshToken { get; private set; }

        public DateTime? RefreshTokenExpiracao { get; private set; }

        public DateTime? UltimoLogin { get; private set; }

        protected Usuario() { }

        public Usuario(
            string nome,
            string email,
            string cpf,
            DateOnly dataNascimento,
            string telefone,
            bool isAdmin,
            string senhaHash)
        {
            DefinirNome(nome);
            DefinirEmail(email);
            DefinirCpf(cpf);
            DefinirDataNascimento(dataNascimento);
            DefinirTelefone(telefone);

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

        public void DefinirDataNascimento(DateOnly dtNasc)
        {
            if (dtNasc == default)
                throw new DomainException("Data Nasc. obrigatória.");

            if (dtNasc >= DateOnly.FromDateTime(DateTime.Today))
                throw new DomainException("Data de nascimento inválida.");
            
            DataNascimento = dtNasc;
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

        private void DefinirCpf(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
                throw new DomainException("Cpf obrigatório.");

            Cpf = new Cpf(cpf);
        }

        public void DefinirTelefone(string telefone)
        {
            if (string.IsNullOrWhiteSpace(telefone))
                throw new DomainException("Telefone obrigatório.");

            Telefone = new Telefone(telefone);
        }
    }
}
