using Florama.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Extensions
{
    public abstract class AuditEntityTypeBuilderExtensions<TEntity> : EntityTypeBuilderExtensions<TEntity> where TEntity : AuditEntity
    {
        /// <summary>
        /// Configuração da entidade base <see cref="AuditEntity" />.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public void Configure(EntityTypeBuilder<TEntity> builder, string table)
        {
            builder.Property(c => c.DataCriacao)
                .HasColumnName("DATACRIACAO")
                .HasColumnType("DATETIME2")
                .IsRequired();

            builder.Property(c => c.UsuarioCriacao)
                .HasColumnName("USUARIOCRIACAO")
                .IsRequired();

            builder.Property(c => c.DataAlteracao)
                .HasColumnName("DATAALTERACAO")
                .HasColumnType("DATETIME2");

            builder.Property(c => c.UsuarioAlteracao)
                .HasColumnName("USUARIOALTERACAO");

            builder.ToTable(table.ToUpper());

            base.Configure(builder);
        }
    }
}
