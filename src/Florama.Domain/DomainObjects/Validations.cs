using System.Text.RegularExpressions;

namespace Florama.Domain.DomainObjects
{
    public class Validations
    {
        public static void ValidarSeVazio(string valor, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(valor))
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeIgual(object object1, object object2, string mensagem)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeDiferente(object object1, object object2, string mensagem)
        {
            if (!object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarTamanhoMaximo(string? valor, int maximo, string mensagem)
        {
            var length = valor?.Trim().Length ?? 0;

            if (!string.IsNullOrEmpty(valor) && length > maximo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarTamanhoMinimo(string valor, int minimo, string mensagem)
        {
            var length = valor?.Trim().Length ?? 0;

            if (length < minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarTamanhoMinimo(int valor, int minimo, string mensagem)
        {
            if (valor < minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarExpressaoRegular(string valor, string expressao, string mensagem)
        {
            if (!Regex.IsMatch(valor, expressao))
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeNulo(object obj, string mensagem)
        {
            if (obj == null)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarMinimoMaximo(decimal valor, decimal minimo, decimal maximo, string mensagem)
        {
            if (valor < minimo || valor > maximo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeMenorIgualMinimo(int valor, int minimo, string mensagem)
        {
            if (valor <= minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeMenorIgualMinimo(decimal valor, decimal minimo, string mensagem)
        {
            if (valor <= minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeMenorIgualMinimo(double valor, double minimo, string mensagem)
        {
            if (valor <= minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeFalso(bool boolValor, string mensagem)
        {
            if (!boolValor)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeVerdadeiro(bool boolValor, string mensagem)
        {
            if (boolValor)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeDataValida(DateTime date, string mensagem)
        {
            if (date == default)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeZero(long valor, string mensagem)
        {
            if (valor == 0)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeMenorQue(DateOnly valor, DateOnly minimo, string mensagem)
        {
            if (valor >= minimo)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarSeMenorQue(long valor, long minimo, string mensagem)
        {
            if (valor <= minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarSeMenorQue(DateTime valor, DateTime comparacao, string mensagem)
        {
            if (valor >= comparacao)
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMaiorQue(long valor, long minimo, string mensagem)
        {
            if (valor > minimo)
            {
                throw new DomainException(mensagem);
            }
        }
        public static void ValidarEmail(string email, string mensagem)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new DomainException(mensagem);

            var regex = new Regex(
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);

            if (!regex.IsMatch(email))
                throw new DomainException(mensagem);
        }
    }

}
