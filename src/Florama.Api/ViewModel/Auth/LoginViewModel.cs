namespace Florama.Api.ViewModel.Auth
{
    public class LoginViewModel
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public DateTime ExpiraEm { get; set; }

        public UsuarioLogadoResponse Usuario { get; set; } = null!;
    }
}
