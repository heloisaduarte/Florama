using Florama.Data.Repositories;
using Florama.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Repositories
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(FloramaDbContext db) : base(db)
        {
        }

        public async Task<Estoque?> ObterPorProdutoIdAsync(long id)
        {
            return await _db.Set<Estoque>()
                .FirstOrDefaultAsync(c => c.ProdutoId == id);
        }
    }
}