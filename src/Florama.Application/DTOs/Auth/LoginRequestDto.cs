namespace Florama.Application.DTOs.Auth
{
    public class LoginRequestDto
    {
        public string Email { get; set; } = null!;

        public string Senha { get; set; } = null!;
    }
}
