using Florama.Domain.DomainObjects;

namespace Florama.Domain.Entities
{
    public class Categoria : AuditEntity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public Categoria(string nome, string descricao)
        {
            Nome = nome;
            Descricao = descricao;
        }
    }
}
