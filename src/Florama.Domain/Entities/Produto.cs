using Florama.Domain.DomainObjects;

namespace Florama.Domain.Entities
{
    public class Produto : AuditEntity
    {
        public string Nome { get; private set; } = null!;
        public string Descricao { get; private set; } = null!;
        public decimal Valor { get; private set; }
        public long CategoriaId { get; private set; }
        public Categoria Categoria { get; private set; } = null!;
        public ICollection<ProdutoFoto> Fotos { get; private set; } = new List<ProdutoFoto>();
        public Estoque Estoque { get; private set; } = null!;
        public Produto()
        {
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

            if (descricao.Length < 3 || descricao.Length > 1000)
                throw new DomainException("Descrição deve ter entre 3 e 1000 caracteres.");

            Descricao = descricao;
        }

        public void DefinirValor(decimal valor)
        {
            if (valor <= 0)
                throw new DomainException("Valor deve ser maior que zero.");

            Valor = valor;
        }

        public void DefinirCategoria(Categoria categoria)
        {
            if (categoria is null)
                throw new DomainException("Categoria é obrigatória.");

            Categoria = categoria;
            CategoriaId = categoria.Id;
        }

        public void AdicionarFoto(string url)
        {
            var ordem = Fotos.Any() ? Fotos.Max(f => f.Ordem) + 1 : 1;
            var foto = new ProdutoFoto();

            foto.DefinirUrl(url);
            foto.DefinirOrdem(ordem); 
            foto.DefinirProduto(this);

            Fotos.Add(foto);

            ValidarFotos();
        }
        public void RemoverFoto(long fotoId)
        {
            var foto = Fotos.FirstOrDefault(f => f.Id == fotoId);

            if (foto is null)
                throw new DomainException("Foto não encontrada.");

            Fotos.Remove(foto);

            ReordenarAutomaticamente();
        }
        public void ReordenarFotos(Dictionary<long, int> novasOrdens)
        {
            foreach (var foto in Fotos)
            {
                if (novasOrdens.TryGetValue(foto.Id, out var novaOrdem))
                {
                    foto.DefinirOrdem(novaOrdem);
                }
            }

            ValidarFotos();
        }
        private void ReordenarAutomaticamente()
        {
            var ordem = 1;

            foreach (var foto in Fotos.OrderBy(f => f.Ordem))
            {
                foto.DefinirOrdem(ordem);
                ordem++;
            }
        }
        public void ValidarFotos()
        {
            var ordensDuplicadas = Fotos
                .GroupBy(f => f.Ordem)
                .Any(g => g.Count() > 1);

            if (ordensDuplicadas)
                throw new DomainException("Não pode haver fotos com a mesma ordem.");
        }
    }
}
