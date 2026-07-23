using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Repositories
{
    public class EstoqueRepository : Repository<Estoque>, IEstoqueRepository
    {
        public EstoqueRepository(FloramaDbContext db) : base(db)
        {
        }

        public async Task<Estoque> ObterPorProdutoId(long id)
        {
            return await _db.Set<Estoque>()
                .Where(c => c.ProdutoId == id)
                .AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
