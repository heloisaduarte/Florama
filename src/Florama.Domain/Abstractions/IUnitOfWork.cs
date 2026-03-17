namespace Florama.Domain.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Salva todas as alterações feitas no contexto do banco de dados.
        /// </summary>
        /// <returns>O número de linhas afetadas no banco de dados.</returns>
        Task<bool> CommitAsync();
    }
}
