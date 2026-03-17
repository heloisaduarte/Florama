using Florama.Data.Extensions;
using Florama.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Mappings
{
    public class CategoriaMapping : AuditEntityTypeBuilderExtensions<Categoria>
    {
        public override void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.Property(u => u.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName(nameof(Categoria.Nome).ToUpper());

            builder.Property(u => u.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasMaxLength(500)
            .HasColumnName(nameof(Categoria.Descricao).ToUpper());

            Configure(builder, nameof(Categoria));
        }
    }
}
