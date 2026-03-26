namespace Florama.Application.Helpers
{
    public static class NumeroPorExtenso
    {
        public static string Converter(double valor)
        {
            if (valor <= 0 || valor >= 1000000000000000)
                return "Valor não suportado pelo sistema.";

            return valor.ToString("C");
        }
    }
}
