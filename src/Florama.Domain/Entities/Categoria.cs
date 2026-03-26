using Florama.Domain.DomainObjects;

namespace Florama.Domain.Entities
{
    public class Categoria : AuditEntity
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }

        public Categoria(string nome, string descricao)
        {
            DefinirNome(nome);
            DefinirDescricao(descricao);
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome é obrigatório.");

            if (nome.Length < 3 || nome.Length > 100)
                throw new DomainException("Nome deve ter entre 3 e 100 caracteres.");

            Nome = nome;
        }

        public void DefinirDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new DomainException("Descrição é obrigatória.");

            if (descricao.Length < 3 || descricao.Length > 500)
                throw new DomainException("Descrição deve ter entre 3 e 500 caracteres.");

            Descricao = descricao;
        }
    }
}
