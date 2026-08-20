using Florama.Data.Repositories;
using Florama.Domain.DomainObjects;
using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(FloramaDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Produto>> ObterEmEstoqueAsync()
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .Include(c => c.Estoque)
                .Where(c => c.Estoque.Quantidade > 0)
                .AsNoTracking().ToListAsync();
        }

        public override async Task<IEnumerable<Produto>> ObterTodosAsync()
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .Include(c => c.Estoque)
                .AsNoTracking().ToListAsync();
        }

        public override async Task<Produto?> ObterPorIdAsync(long id)
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .Include(c => c.Estoque)
                .AsTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}