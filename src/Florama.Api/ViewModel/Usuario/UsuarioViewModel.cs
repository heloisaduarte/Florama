namespace Florama.Api.ViewModel.Usuario
{
    public class UsuarioViewModel
    {
        public long Id { get; set; }

        public string Nome { get; set; } = null!;

        public string Email { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
