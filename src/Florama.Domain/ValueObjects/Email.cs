using Florama.Domain.DomainObjects;
using System.Text.RegularExpressions;

namespace Florama.Domain.ValueObjects
{
    public class Email
    {
        public string Endereco { get; }
        protected Email()
        {
        }
        public Email(string email)
        {
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                throw new DomainException("Email inválido");

            Endereco = email;
        }
    }
}
