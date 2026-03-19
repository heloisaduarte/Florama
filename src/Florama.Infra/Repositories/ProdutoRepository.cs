using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;

namespace Florama.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly FloramaDbContext _db;

        public ProdutoRepository(FloramaDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
