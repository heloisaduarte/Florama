namespace Florama.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public DateTime ExpiraEm { get; set; }

        public UsuarioLogadoDto Usuario { get; set; } = null!;
    }
}
