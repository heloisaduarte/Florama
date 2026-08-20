namespace Florama.Application.DTOs.Auth
{
    public class UsuarioLogadoDto
    {
        public long Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
