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
        /// <summary>
        /// The db set of this repository
        /// </summary>
        private readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Initialize a new <see cref="RepositoryBase{TEntity, TIdentifier}"/>
        /// </summary>
        /// <param name="databaseContext">The database context</param>
        public RepositoryBase(DbContext databaseContext)
        {
            if (databaseContext == null) throw new ArgumentNullException(nameof(databaseContext));

            // We don't give access to the data context to prevent usage of SaveChanges() and other stuff
            // So we only store DbSet
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

        /// <summary>
        /// Add an entity
        /// </summary>
        /// <param name="entityToCreate">The entity to add</param>
        /// <returns>The new entity identifier</returns>
        public async Task<TIdentifier> AddAsync(TEntity entityToCreate)
        {
            var entry = await _dbSet.AddAsync(entityToCreate);
            
            return entry.Entity.Id;
        }
    }
}
