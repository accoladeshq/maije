using System;
using System.Threading;
using System.Threading.Tasks;
using Accolades.Maije.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Accolades.Maije.Distributed.Health
{
    internal class MaijeDbContextHealthCheck : IHealthCheck
    {
        private static readonly Func<DbContext, CancellationToken, Task<bool>> DefaultTestQuery = (dbContext, cancellationToken) =>
        {
            return dbContext.Database.CanConnectAsync(cancellationToken);
        };

        /// <summary>
        /// The data context
        /// </summary>
        private readonly DbContext _dbContext;

        /// <summary>
        /// Initialize a new <see cref="MaijeDbContextHealthCheck"/>
        /// </summary>
        /// <param name="maijeDbContext">The maije data context</param>
        public MaijeDbContextHealthCheck(IMaijeDbContext maijeDbContext)
        {
            _dbContext = maijeDbContext as DbContext;

            if (_dbContext == null)
                throw new ArgumentException("The IMaijeDbContext must implement DbContext");
        }

        /// <summary>
        /// Check health for the component
        /// </summary>
        /// <param name="context">The context</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var testQuery = DefaultTestQuery;

            if (await testQuery(_dbContext, cancellationToken))
            {
                return HealthCheckResult.Healthy();
            }

            return HealthCheckResult.Unhealthy();
        }
    }
}
