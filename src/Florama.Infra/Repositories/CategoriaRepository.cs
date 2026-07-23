using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;

namespace Florama.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(FloramaDbContext db) : base(db)
        {
        }
    }
}
