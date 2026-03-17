using Florama.Application.Interfaces;
using Florama.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;

namespace Florama.Infrastructure.Data
{
    public class FloramaDbContext : DbContext
    {
        private readonly IUsuarioLogado _usuarioLogado;

        public FloramaDbContext(
            DbContextOptions<FloramaDbContext> options,
            IUsuarioLogado usuarioLogado)
            : base(options)
        {
            _usuarioLogado = usuarioLogado;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FloramaDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAudit();

            return await base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAudit()
        {
            var entries = ChangeTracker
                .Entries<AuditEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DataCriacao = DateTime.UtcNow;
                    entry.Entity.UsuarioCriacao = _usuarioLogado.UsuarioId;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.DataAlteracao = DateTime.UtcNow;
                    entry.Entity.UsuarioAlteracao = _usuarioLogado.UsuarioId;
                }
            }
        }
    }
}