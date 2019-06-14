using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure
{
    public abstract class RepositoryBase<TEntity, TIdentifier>
        where TEntity : class, IIdentity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext databaseContext)
        {
            if (databaseContext == null) throw new ArgumentNullException(nameof(databaseContext));

            _dbSet = databaseContext.Set<TEntity>();
        }

        /// <summary>
        /// Gets all repository items
        /// </summary>
        /// <returns>Return all items from the database</returns>
        public async Task<IEnumerable<TEntity>> GetItemsAsync()
        {
            return await _dbSet.ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets items by it's identifier
        /// </summary>
        /// <param name="identifier">The item identifier</param>
        /// <returns></returns>
        public async Task<TEntity> GetItemByIdAsync(TIdentifier identifier)
        {
            var entity =  await _dbSet.FirstOrDefaultAsync(e => e.Id.Equals(identifier)).ConfigureAwait(false);

            if(entity == null)
            {
                throw new InfrastructureException($"Cannot find entity of type {typeof(TEntity)} with identifier {identifier}");
            }

            return entity;
        }
    }
}
