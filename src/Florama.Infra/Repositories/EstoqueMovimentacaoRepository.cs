using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;

namespace Florama.Data.Repositories
{
    public class EstoqueMovimentacaoRepository : Repository<EstoqueMovimentacao>, IEstoqueMovimentacaoRepository
    {
        public EstoqueMovimentacaoRepository(FloramaDbContext db) : base(db)
        {
        }
    }
}
