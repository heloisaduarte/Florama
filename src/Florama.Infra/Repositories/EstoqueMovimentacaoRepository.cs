using Florama.Data.Repositories;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;

namespace Florama.Data.Repositories
{
    public class EstoqueMovimentacaoRepository : Repository<EstoqueMovimentacao>, IEstoqueMovimentacaoRepository
    {
        public EstoqueMovimentacaoRepository(FloramaDbContext db) : base(db)
        {
        }
    }
}
