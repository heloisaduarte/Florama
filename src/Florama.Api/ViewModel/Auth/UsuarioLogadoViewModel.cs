namespace Florama.Api.ViewModel.Auth
{
    public class UsuarioLogadoResponse
    {
        public long Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
