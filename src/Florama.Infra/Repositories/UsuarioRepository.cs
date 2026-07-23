using Florama.Domain.Entities;
using Florama.Domain.Interfaces.Repositories;
using Florama.Infra.Repositories;
using Florama.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(FloramaDbContext db) : base(db)
        {
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _db.Set<Usuario>()
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email.Endereco == email);
        }

        public async Task<Usuario?> ObterPorRefreshTokenAsync(string refreshToken)
        {
            return await _db.Set<Usuario>()
                .FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
        }
    }
}
