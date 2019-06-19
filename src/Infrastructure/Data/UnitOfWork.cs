using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The application data context
        /// </summary>
        private readonly IMaijeDbContext _dbContext;

        /// <summary>
        /// The available repositories
        /// </summary>
        private readonly IEnumerable<IRepositoryBase> _availableRepositories;

        /// <summary>
        /// To detect redundant calls
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// Initialize an <see cref="UnitOfWorkBase"/>
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(IMaijeDbContext dbContext, IEnumerable<IRepositoryBase> repositories)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _availableRepositories = repositories ?? throw new ArgumentNullException(nameof(repositories));
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="T">Type of the repository</typeparam>
        /// <returns></returns>
        public T GetRepository<T>() where T : IRepositoryBase
        {
            var repositoryType = typeof(T);

            return (T)GetRepository(repositoryType);
        }

        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <param name="repositoryType">Type of the repository.</param>
        /// <returns></returns>
        /// <exception cref="Exception">The repository not exists or not inherit from IRepositoryBase</exception>
        public IRepositoryBase GetRepository(Type repositoryType)
        {
            var repository = _availableRepositories.FirstOrDefault(r => repositoryType.IsAssignableFrom(r.GetType()));

            if (repository == null)
            {
                throw new InfrastructureException($"No type {repositoryType.Name} found");
            }

            return repository;
        }

        /// <summary>
        /// Commit all changes mades in the context to the database
        /// </summary>
        /// <returns>The numbers of lines commited</returns>
        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose class resources
        /// </summary>
        /// <param name="disposing">If the class is currently disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }

                disposedValue = true;
            }
        }

        /// <summary>
        /// Dispose resource of this class
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
