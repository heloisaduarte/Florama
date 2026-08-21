using Florama.Data.Extensions;
using Florama.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Florama.Data.Mappings
{
    public class UsuarioMapping : AuditEntityTypeBuilderExtensions<Usuario>
    {
        public override void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.Property(a => a.Nome)
                .HasColumnType("varchar(200)")
                .IsRequired()
                .HasColumnName(nameof(Usuario.Nome).ToUpper());

            builder.OwnsOne(a => a.Email, email =>
            {
                email.Property(x => x.Endereco)
                    .HasColumnType("varchar(200)")
                    .HasColumnName(nameof(Usuario.Email).ToUpper())
                    .IsRequired();
            });

            builder.Property(a => a.IsAdmin)
                .IsRequired()
                .HasColumnType("bit")
                .HasColumnName(nameof(Usuario.IsAdmin).ToUpper());

            builder.Property(x => x.SenhaHash)
                .HasColumnName(nameof(Usuario.SenhaHash).ToUpper())
                .HasColumnType("varchar(255)")
                .IsRequired();

            builder.Property(x => x.Ativo)
                .HasColumnName(nameof(Usuario.Ativo).ToUpper())
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.EmailConfirmado)
                .HasColumnName(nameof(Usuario.EmailConfirmado).ToUpper())
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(x => x.RefreshToken)
                .HasColumnName(nameof(Usuario.RefreshToken).ToUpper())
                .HasColumnType("varchar(500)");

            builder.Property(x => x.RefreshTokenExpiracao)
                .HasColumnName(nameof(Usuario.RefreshTokenExpiracao).ToUpper());

            builder.Property(x => x.UltimoLogin)
                .HasColumnName(nameof(Usuario.UltimoLogin).ToUpper());

            builder.Property(x => x.TentativasLoginFalhas)
                .HasColumnName(nameof(Usuario.TentativasLoginFalhas).ToUpper())
                .HasColumnType("INT")
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(x => x.BloqueioAte)
                .HasColumnName(nameof(Usuario.BloqueioAte).ToUpper());

            builder.ToTable(nameof(Usuario).ToUpper());

            base.Configure(builder);
        }
    }
}
