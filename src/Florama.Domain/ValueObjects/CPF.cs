using Florama.CrossCutting.Extensions;
using Florama.Domain.DomainObjects;

namespace Florama.Domain.ValueObjects
{
    public class Cpf : IEquatable<Cpf>
    {
        public const int CpfMaxLength = 11;

        public string CpfLimpo { get; private set; } = null!;

        protected Cpf()
        {
        }

        public Cpf(string numero)
        {
            if (!Validar(numero))
            {
                throw new DomainException("CPF inválido");
            }

            CpfLimpo = numero.ApenasNumeros();
        }

        public string ObterCpfFormatado()
        {
            return long.Parse(CpfLimpo).ToString("000\\.000\\.000\\-00");
        }

        public bool Equals(Cpf? other)
        {
            if (other is null) return false;

            return CpfLimpo == other.CpfLimpo;
        }

        public override bool Equals(object? obj)
        {
            return obj is Cpf cpf && Equals(cpf);
        }

        public override int GetHashCode()
        {
            return CpfLimpo?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return ObterCpfFormatado();
        }

        public static bool operator ==(Cpf? a, Cpf? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Cpf? a, Cpf? b)
        {
            return !(a == b);
        }

        public static bool Validar(string cpf)
        {
            cpf = cpf.ApenasNumeros();
            if (cpf.Length > 11)
            {
                return false;
            }

            while (cpf.Length != 11)
            {
                cpf = "0" + cpf;
            }

            bool flag = true;
            for (int i = 1; i < 11 && flag; i++)
            {
                if (cpf[i] != cpf[0])
                {
                    flag = false;
                }
            }

            if (flag || cpf == "12345678909")
            {
                return false;
            }

            int[] array = new int[11];
            for (int j = 0; j < 11; j++)
            {
                array[j] = int.Parse(cpf[j].ToString());
            }

            int num = 0;
            for (int k = 0; k < 9; k++)
            {
                num += (10 - k) * array[k];
            }

            int num2 = num % 11;
            if (num2 == 1 || num2 == 0)
            {
                if (array[9] != 0)
                {
                    return false;
                }
            }
            else if (array[9] != 11 - num2)
            {
                return false;
            }

            num = 0;
            for (int l = 0; l < 10; l++)
            {
                num += (11 - l) * array[l];
            }

            num2 = num % 11;
            if (num2 == 1 || num2 == 0)
            {
                if (array[10] != 0)
                {
                    return false;
                }
            }
            else if (array[10] != 11 - num2)
            {
                return false;
            }

            return true;
        }
    }
}