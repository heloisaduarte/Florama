namespace Florama.Api.ViewModel.Auth
{
    public class LoginViewModel
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public DateTime ExpiraEm { get; set; }

        public UsuarioLogadoResponse Usuario { get; set; }
    }
}
