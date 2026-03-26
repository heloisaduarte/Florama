using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Florama.CrossCutting.Extensions
{
    public static class StringExtensions
    {
        public static bool IsInteger(this string texto)
        {
            return int.TryParse(texto?.Trim(), out _);
        }

        public static bool IsDate(this string texto)
        {
            return DateTime.TryParse(texto?.Trim(), out _);
        }

        public static bool IsEmail(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return false;

            return Regex.IsMatch(texto,
                @"^[A-Za-z0-9.!#$%&'*+/=?^_`{|}~-]+@[A-Za-z0-9-]+(\.[A-Za-z0-9-]+)+$");
        }

        public static string RemoverEspacos(this string texto)
        {
            if (texto is null) return "";
            return Regex.Replace(texto, @"\s+", "");
        }

        public static string ApenasNumeros(this string texto)
        {
            if (string.IsNullOrEmpty(texto)) return "";
            return new string(texto.Where(char.IsDigit).ToArray());
        }

        public static string RemoverCaracteres(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
            return Regex.Replace(texto.Trim(), @"[^0-9]", "");
        }

        public static string RemoverNumero(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return string.Empty;
            return Regex.Replace(texto.Trim(), @"\d+", "");
        }

        public static string PrimeiraMaiuscula(this string texto)
        {
            if (string.IsNullOrEmpty(texto)) return texto;
            return char.ToUpper(texto[0]) + texto.Substring(1);
        }

        public static string RemoverAcentos(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return string.Empty;

            var normalized = texto.Normalize(NormalizationForm.FormD);
            return new string(normalized
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray())
                .Normalize(NormalizationForm.FormC);
        }

        public static string ToTitleCasePtBr(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return texto;

            texto = texto.Trim().ToLower();

            var textInfo = new CultureInfo("pt-BR").TextInfo;
            var resultado = textInfo.ToTitleCase(texto);

            string[] minusculas = { "da", "de", "do", "das", "dos" };

            var palavras = resultado.Split(' ');
            for (int i = 0; i < palavras.Length; i++)
            {
                var p = palavras[i].ToLower();
                if (minusculas.Contains(p))
                    palavras[i] = p;
            }

            return string.Join(" ", palavras);
        }

        public static bool ValidarNumeroString(this string numero)
        {
            return numero != null && Regex.IsMatch(numero, @"^-?\d+$");
        }

        public static bool ValidarHoraString(this string hora)
        {
            return hora != null && Regex.IsMatch(hora, @"^([01][0-9]|2[0-3]):([0-5][0-9])$");
        }

        public static bool ValidarDataString(this string data)
        {
            return data != null && Regex.IsMatch(data, @"^(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[0-2])/\d{4}$");
        }
    }
}
