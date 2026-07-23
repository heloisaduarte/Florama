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
        public ProdutoRepository(FloramaDbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Produto>> ObterEmEstoque()
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .Include(c => c.Estoque)
                .Where(c => c.Estoque.Quantidade > 0)
                .AsNoTracking().ToListAsync();

        }
        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .Include(c => c.Estoque)
                .AsNoTracking().ToListAsync();

        }
        public async Task<Produto> ObterPorId(long Id)
        {
            return await _db.Set<Produto>()
                .Include(c => c.Fotos.OrderBy(f => f.Ordem))
                .Include(c => c.Estoque)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == Id);

        }
    }
}
