using Florama.Data.Extensions;
using Florama.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Mappings
{
    public class ProdutoMapping : AuditEntityTypeBuilderExtensions<Produto>
    {
        public override void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.Property(u => u.Nome)
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasMaxLength(100)
            .HasColumnName(nameof(Produto.Nome).ToUpper());

            builder.Property(u => u.Descricao)
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName(nameof(Produto.Descricao).ToUpper());

            builder.Property(u => u.Valor)
            .HasColumnType("DECIMAL")
            .IsRequired()
            .HasColumnName(nameof(Produto.Valor).ToUpper());

            builder.Property(x => x.CategoriaId)
               .HasColumnName(nameof(Produto.CategoriaId).ToUpper())
               .IsRequired();

            builder.HasOne(x => x.Categoria)
                .WithMany()
                .HasForeignKey(x => x.CategoriaId);

            Configure(builder, nameof(Produto));
        }
    }
}
