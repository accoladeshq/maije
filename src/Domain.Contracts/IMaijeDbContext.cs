using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Accolades.Maije.Domain.Contracts
{
    public interface IMaijeDbContext : IDisposable
    {
        /// <summary>
        /// Save current changes to database
        /// </summary>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Gets the context data for a selected entity
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IEnumerable<TEntity> Set<TEntity>() where TEntity : class; // We use IEnumerable to prevent dependency with EntityFrameworkCore in domain
    }
}
