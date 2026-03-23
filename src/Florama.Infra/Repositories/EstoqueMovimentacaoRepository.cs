using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;

namespace Florama.Data.Repositories
{
    public class EstoqueMovimentacaoRepository : Repository<EstoqueMovimentacao>, IEstoqueMovimentacaoRepository
    {
        private readonly FloramaDbContext _db;

        public EstoqueMovimentacaoRepository(FloramaDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
