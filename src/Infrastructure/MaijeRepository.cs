using Accolades.Maije.Crosscutting.Exceptions;
using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure
{
    public class MaijeRepository<TEntity, TIdentifier> : IMaijeRepository<TEntity, TIdentifier>
        where TEntity : class, IIdentity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// The db set of this repository
        /// </summary>
        protected readonly DbSet<TEntity> DbSet;

        /// <summary>
        /// Initialize a new <see cref="MaijeRepository{TEntity, TIdentifier}"/>
        /// </summary>
        /// <param name="databaseContext">The database context</param>
        public MaijeRepository(IMaijeDbContext databaseContext)
        {
            if (databaseContext == null)
                throw new ArgumentNullException(nameof(databaseContext));

            // We don't give access to the data context to prevent usage of SaveChanges() and other stuff
            // So we only store DbSet
            DbSet = databaseContext.Set<TEntity>() as DbSet<TEntity>; // Ugly cast, maybe wa can find a better way

            if(DbSet == null)
            {
                throw new InfrastructureException($"The {nameof(databaseContext)} must be a MaijeDbContext to work with repository.");
            }
        }

        /// <summary>
        /// Gets all repository items
        /// </summary>
        /// <returns>Return all items from the database</returns>
        public async Task<IEnumerable<TEntity>> GetItemsAsync()
        {
            return await GetItemsQuery(false).ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Gets items by it's identifier
        /// </summary>
        /// <param name="identifier">The item identifier</param>
        /// <returns></returns>
        public Task<TEntity> GetItemByIdAsync(TIdentifier identifier)
        {
            return GetItemByIdAsync(identifier, false);
        }

        /// <summary>
        /// Gets item by it's identifier
        /// </summary>
        /// <param name="identifier">The entity identifier</param>
        /// <param name="trackable">If the entity is tracked by ORM</param>
        /// <returns></returns>
        public async Task<TEntity> GetItemByIdAsync(TIdentifier identifier, bool trackable)
        {
            var entity = await GetByIdQuery(identifier, trackable).FirstOrDefaultAsync().ConfigureAwait(false);

            if (entity == null)
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
            var entry = await DbSet.AddAsync(entityToCreate).ConfigureAwait(false);
            
            return entry.Entity.Id;
        }

        /// <summary>
        /// Get paginated items
        /// </summary>
        /// <typeparam name="TProjectedEntity">The projection type</typeparam>
        /// <param name="paginationRequest">The pagination request</param>
        /// <param name="projection">The project to apply</param>
        /// <returns></returns>
        public async Task<PaginationResult<TEntity>> GetPaginatedAsync(PaginationRequest paginationRequest)
        {
            var paginatedQuery = GetPaginationQuery(paginationRequest);

            var items = await paginatedQuery.ToListAsync().ConfigureAwait(false);

            return new PaginationResult<TEntity>(items, paginationRequest.Offset, 2, paginationRequest.Limit, null);
        }

        /// <summary>
        /// Update an entity
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns></returns>
        public Task<TEntity> UpdateAsync(TEntity entityToUpdate)
        {
            var entry = DbSet.Update(entityToUpdate);

            return Task.FromResult(entry.Entity);
        }

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="identifier">The identifier to delete</param>
        /// <returns></returns>
        public virtual Task DeleteAsync(TIdentifier identifier)
        {
            var entityToRemove = CreateDeleteEntity(identifier);

            DbSet.Remove(entityToRemove);

            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Gets the pagination query
        /// </summary>
        /// <param name="paginationRequest">The pagination request</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetPaginationQuery(PaginationRequest paginationRequest)
        {
            IQueryable<TEntity> query = DbSet.AsNoTracking();

            query = query.Skip(paginationRequest.Offset).Take(paginationRequest.Limit);

            return query;
        }

        /// <summary>
        /// Get the by identifier query
        /// </summary>
        /// <param name="identifier">The entity identifier</param>
        /// <param name="trackable">If the entity is trackable</param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetByIdQuery(TIdentifier identifier, bool trackable)
        {
            IQueryable<TEntity> query = null;

            if (trackable)
                query = DbSet.Where(e => e.Id.Equals(identifier));
            else
                query = DbSet.Where(e => e.Id.Equals(identifier)).AsNoTracking();

            return query;
        }


        /// <summary>
        /// Gets the items query
        /// </summary>
        /// <param name="trackable"></param>
        /// <returns></returns>
        protected virtual IQueryable<TEntity> GetItemsQuery(bool trackable)
        {
            IQueryable<TEntity> query;

            if (trackable)
                query = DbSet;
            else
                query = DbSet.AsNoTracking();

            return query;
        }

        /// <summary>
        /// Create an entity with only it's identifier
        /// </summary>
        /// <param name="id">The entity identifier</param>
        /// <returns></returns>
        protected TEntity CreateDeleteEntity(TIdentifier id)
        {
            var entityType = typeof(TEntity);

            // Get the constructor info for these parameters
            var constructorInfo = entityType.GetConstructor(BindingFlags.CreateInstance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance, null, new Type[] { typeof(TIdentifier) }, null);

            if (constructorInfo == null)
            {
                throw new InfrastructureException($"You need to have a constructor with only {typeof(TIdentifier)} as parameter");
            }

            var paramExpr = Expression.Parameter(typeof(TIdentifier));
            
            var body = Expression.New(constructorInfo, paramExpr);

            var constructor = Expression.Lambda<ConstructorDelegate>(body, paramExpr);
            var c = constructor.Compile();

            return c(id);
        }

        /// <summary>
        /// The constructor delegate
        /// </summary>
        /// <param name="args">Arguments</param>
        /// <returns></returns>
        private delegate TEntity ConstructorDelegate(TIdentifier args);
    }
}
