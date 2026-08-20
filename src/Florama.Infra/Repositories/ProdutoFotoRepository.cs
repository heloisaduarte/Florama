using Florama.Data.Repositories;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;

namespace Florama.Data.Repositories
{
    public class ProdutoFotoRepository : Repository<ProdutoFoto>, IProdutoFotoRepository
    {
        public ProdutoFotoRepository(FloramaDbContext db) : base(db)
        {
        }
    }
}
