namespace Florama.Api.ViewModel.Auth
{
    public class UsuarioLogadoResponse
    {
        public long Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
