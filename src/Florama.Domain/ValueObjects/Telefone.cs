using Florama.CrossCutting.Extensions;
using Florama.Domain.DomainObjects;

namespace Florama.Domain.ValueObjects
{
    public class Telefone : IEquatable<Telefone>
    {
        public string Numero { get; } = null!;

        protected Telefone()
        {
        }

        public Telefone(string numero)
        {
            Numero = numero.ApenasNumeros();

            if (Numero.Length < 10 || Numero.Length > 11)
                throw new DomainException("Telefone inválido. Informe DDD + número.");
        }

        public bool Equals(Telefone? other)
        {
            if (other is null) return false;

            return Numero == other.Numero;
        }

        public override bool Equals(object? obj)
        {
            return obj is Telefone telefone && Equals(telefone);
        }

        public override int GetHashCode()
        {
            return Numero?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return Numero;
        }

        public static bool operator ==(Telefone? a, Telefone? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Telefone? a, Telefone? b)
        {
            return !(a == b);
        }
    }
}