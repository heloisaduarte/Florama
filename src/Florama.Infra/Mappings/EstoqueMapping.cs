using Florama.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Mappings
{
    public class EstoqueMapping : AuditEntityTypeBuilderExtensions<Estoque>
    {
        public override void Configure(EntityTypeBuilder<Estoque> builder)
        {
            builder.Property(u => u.Quantidade)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName(nameof(Estoque.Quantidade).ToUpper());

            builder.Property(x => x.ProdutoId)
               .HasColumnName(nameof(Estoque.ProdutoId).ToUpper())
               .IsRequired();

            builder.HasOne(x => x.Produto)
    .WithOne(p => p.Estoque)
    .HasForeignKey<Estoque>(x => x.ProdutoId);

            Configure(builder, nameof(Estoque));
        }
    }
}
