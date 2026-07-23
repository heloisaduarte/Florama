namespace Florama.Application.DTOs.Auth
{
    public class UsuarioLogadoDto
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
