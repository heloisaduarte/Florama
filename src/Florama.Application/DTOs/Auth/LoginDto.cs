namespace Florama.Application.DTOs.Auth
{
    public class LoginDto
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiraEm { get; set; }

        public UsuarioLogadoDto Usuario { get; set; }
    }
}
