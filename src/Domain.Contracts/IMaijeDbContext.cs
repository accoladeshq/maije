using System;
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
    }
}
