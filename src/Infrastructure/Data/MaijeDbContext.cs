using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure.Data
{
    public abstract class MaijeDbContext : DbContext, IMaijeDbContext
    {
        /// <summary>
        /// Initialize a new <see cref="MaijeDbContext"/>
        /// </summary>
        /// <param name="options">The data context options</param>
        public MaijeDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Initialize a new <see cref="MaijeDbContext"/>
        /// </summary>
        protected MaijeDbContext()
        {
        }

        /// <summary>
        /// Save the current changes
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            try
            {
                return base.SaveChanges(acceptAllChangesOnSuccess);
            }
            catch (Exception e)
            {
                throw new InfrastructureException(e.Message, e);
            }
        }

        /// <summary>
        /// Save current changes
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            try
            {
                var nbOfLines = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken).ConfigureAwait(false);

                return nbOfLines;
            }
            catch (Exception e)
            {
                throw new InfrastructureException(e.Message, e);
            }
        }
    }
}
