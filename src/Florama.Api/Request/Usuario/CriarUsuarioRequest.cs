namespace Florama.Api.Request.Usuario
{
    public class CriarUsuarioRequest
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public DateOnly DataNascimento { get; set; }

        public string Telefone { get; set; } = null!;

        public string Senha { get; set; } = null!;

        public string ConfirmarSenha { get; set; } = null!;
    }
}
