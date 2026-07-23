using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;

namespace Florama.Data.Repositories
{
    public class ProdutoFotoRepository : Repository<ProdutoFoto>, IProdutoFotoRepository
    {
        public ProdutoFotoRepository(FloramaDbContext db) : base(db)
        {
        }
    }
}
