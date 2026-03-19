using Florama.Data.Extensions;
using Florama.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Mappings
{
    public class ProdutoFotoMapping : AuditEntityTypeBuilderExtensions<ProdutoFoto>
    {
        public override void Configure(EntityTypeBuilder<ProdutoFoto> builder)
        {
            builder.Property(u => u.Url)
            .HasColumnType("VARCHAR")
            .IsRequired()
            .HasMaxLength(1000)
            .HasColumnName(nameof(ProdutoFoto.Url).ToUpper());

            builder.Property(u => u.Ordem)
            .HasColumnType("INT")
            .IsRequired()
            .HasColumnName(nameof(ProdutoFoto.Ordem).ToUpper());

            builder.Property(x => x.ProdutoId)
               .HasColumnName(nameof(ProdutoFoto.ProdutoId).ToUpper())
               .IsRequired();

            builder.HasOne(x => x.Produto)
                .WithMany(p => p.Fotos)
                .HasForeignKey(x => x.ProdutoId);

            Configure(builder, nameof(ProdutoFoto));
        }
    }
}
