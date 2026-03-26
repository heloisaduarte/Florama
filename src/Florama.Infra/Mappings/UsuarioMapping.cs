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

            builder.OwnsOne(a => a.Telefone, telefone =>
            {
                telefone.Property(x => x.Numero)
                    .HasColumnType("varchar(11)")
                    .HasColumnName(nameof(Usuario.Telefone).ToUpper())
                    .IsRequired();
            });

            builder.OwnsOne(a => a.Cpf, cpf =>
            {
                cpf.Property(x => x.CpfLimpo)
                    .HasColumnType("varchar(11)")
                    .HasColumnName(nameof(Usuario.Cpf).ToUpper())
                    .IsRequired();
            });

            builder.Property(a => a.DataNascimento)
                .IsRequired()
                .HasColumnType("date")
                .HasColumnName(nameof(Usuario.DataNascimento).ToUpper());

            builder.Property(a => a.IsAdmin)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName(nameof(Usuario.IsAdmin).ToUpper());

            builder.ToTable(nameof(Usuario).ToUpper());

            base.Configure(builder);
        }
    }
}
