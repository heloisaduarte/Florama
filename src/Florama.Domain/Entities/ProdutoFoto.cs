using Florama.Domain.DomainObjects;

namespace Florama.Domain.Entities
{
    public class ProdutoFoto : AuditEntity
    {
        public long ProdutoId { get; private set; }
        public Produto Produto { get; private set; }
        public string Url { get; private set; }
        public int Ordem { get; private set; }

        public ProdutoFoto()
        {
        }

        public void DefinirProduto(Produto produto)
        {
            if (produto is null)
                throw new DomainException("Produto é obrigatório.");

            Produto = produto;
            ProdutoId = produto.Id;
        }

        public void DefinirUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
                throw new DomainException("Url da foto é obrigatória.");

            if (url.Length > 500)
                throw new DomainException("Url muito longa.");

            Url = url;
        }

        public void DefinirOrdem(int ordem)
        {
            if (ordem <= 0)
                throw new DomainException("Ordem deve ser maior que zero.");

            Ordem = ordem;
        }
    }
}