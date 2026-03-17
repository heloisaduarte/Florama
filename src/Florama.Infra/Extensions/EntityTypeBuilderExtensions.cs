using Florama.Domain.DomainObjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Florama.Data.Extensions
{
    public abstract class EntityTypeBuilderExtensions<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Configuração da entidade base <see cref="Entity" />.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(entity => entity.Id);

            builder.Property(entity => entity.Id)
                .IsRequired()
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();
        }
    }
}
