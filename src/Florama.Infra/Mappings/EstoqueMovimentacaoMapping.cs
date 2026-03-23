using Florama.Data.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Mappings
{
    public class EstoqueMovimentacaoMapping : AuditEntityTypeBuilderExtensions<EstoqueMovimentacao>
    {
        public override void Configure(EntityTypeBuilder<EstoqueMovimentacao> builder)
        {
            builder.Property(u => u.Quantidade)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName(nameof(EstoqueMovimentacao.Quantidade).ToUpper());

            builder.Property(x => x.ProdutoId)
               .HasColumnName(nameof(EstoqueMovimentacao.ProdutoId).ToUpper())
               .IsRequired();

            builder.HasOne(x => x.Produto)
                .WithMany()
                .HasForeignKey(x => x.ProdutoId);

            builder.Property(x => x.Tipo)
               .HasColumnName(nameof(EstoqueMovimentacao.Tipo).ToUpper())
               .HasColumnType("INT")
                .IsRequired();
            
            Configure(builder, nameof(EstoqueMovimentacao));
        }
    }
}
