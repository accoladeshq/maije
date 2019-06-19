using System;
using System.Threading.Tasks;

namespace Accolades.Maije.Domain.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Commit all transactions from the repositories
        /// </summary>
        Task<int> CommitAsync();

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TI">The type of the i.</typeparam>
        /// <returns></returns>
        T GetRepository<T>() where T : IRepositoryBase;

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <param name="repositoryType">Type of the repository.</param>
        /// <returns></returns>
        IRepositoryBase GetRepository(Type repositoryType);
    }
}
