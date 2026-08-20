using Florama.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FloramaDbContext _context;

        public UnitOfWork(FloramaDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}