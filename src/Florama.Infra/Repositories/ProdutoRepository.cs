using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly FloramaDbContext _db;

        public ProdutoRepository(FloramaDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .AsNoTracking().ToListAsync();

        }
        public async Task<Produto> ObterPorId(long Id)
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .AsNoTracking().FirstOrDefaultAsync();

        }
    }
}
