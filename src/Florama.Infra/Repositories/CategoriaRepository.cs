using Florama.Data.Repositories;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;

namespace Florama.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(FloramaDbContext db) : base(db)
        {
        }
    }
}
