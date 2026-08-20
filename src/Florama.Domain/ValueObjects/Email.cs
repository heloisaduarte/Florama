using Florama.Domain.DomainObjects;
using System.Text.RegularExpressions;

namespace Florama.Domain.ValueObjects
{
    public class Email : IEquatable<Email>
    {
        public string Endereco { get; } = null!;

        protected Email()
        {
        }

        public Email(string email)
        {
            if (string.IsNullOrWhiteSpace(email) ||
                !Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainException("Email inválido");

            Endereco = email;
        }

        public bool Equals(Email? other)
        {
            if (other is null) return false;

            return string.Equals(Endereco, other.Endereco, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            return obj is Email email && Equals(email);
        }

        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Endereco);
        }

        public override string ToString()
        {
            return Endereco;
        }

        public static bool operator ==(Email? a, Email? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Email? a, Email? b)
        {
            return !(a == b);
        }
    }
}