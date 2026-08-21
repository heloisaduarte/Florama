namespace Florama.Application.DTOs.Auth
{
    public class CriarUsuarioDto
    {
        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;
    }
}
