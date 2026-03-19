using Florama.Domain.DomainObjects;

namespace Florama.Domain.Entities
{
    public class Produto : AuditEntity
    {
        public string Nome { get; private set; }
        public decimal Valor { get; private set; }
        public long CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; }

        public Produto()
        {
        }

        public void DefinirNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("Nome é obrigatório");

            if (nome.Length < 3 || nome.Length > 100)
                throw new DomainException("Nome deve ter entre 3 e 100 caracteres");

            Nome = nome;
        }

        public void DefinirValor(decimal valor)
        {
            if (valor <= 0)
                throw new DomainException("Valor deve ser maior que zero");

            Valor = valor;
        }

        public void DefinirCategoria(Categoria categoria)
        {
            if (categoria is null)
                throw new DomainException("Categoria é obrigatória");

            Categoria = categoria;
            CategoriaId = categoria.Id;
        }
    }
}
