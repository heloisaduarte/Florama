using Florama.CrossCutting.Extensions;
using Florama.Domain.DomainObjects;

namespace Florama.Domain.ValueObjects
{
    public class Telefone
    {
        public string Numero { get; }
        protected Telefone()
        {
        }
        public Telefone(string numero)
        {
            Numero = numero.ApenasNumeros();

            if (Numero.Length < 10 || Numero.Length > 11)
                throw new DomainException("Telefone inválido. Informe DDD + número.");
        }
    }
}
