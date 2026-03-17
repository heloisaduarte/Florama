using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;

namespace Florama.Data.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        private readonly FloramaDbContext _db;

        public CategoriaRepository(FloramaDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
