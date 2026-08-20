namespace Florama.Domain.Options
{
    public class LoginOptions
    {
        public int MaximoTentativas { get; set; } = 5;

        public int BloqueioMinutos { get; set; } = 15;
    }
}