namespace Florama.Domain.DomainObjects
{
    /// <summary>
    /// Entidade com a chave (PK) tipada em <see cref="long"/>.
    /// </summary>
    public abstract class AuditEntity : Entity
    {
        public const int MAX_LENGTH_NOME = 200;
        public const int MAX_LENGTH_DESCRICAO = 1000;

        public DateTime DataCriacao { get; set; }
        public long UsuarioCriacao { get; set; }
        public DateTime? DataAlteracao { get; set; }
        public long? UsuarioAlteracao { get; set; }
    }
}
