using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Domain.Entities.Commons;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure
{
    public class DeactivatableRepositoryBase<TEntity, TIdentifier> : RepositoryBase<TEntity, TIdentifier>
        where TEntity : class, IIdentity<TIdentifier>, IDeactivatable
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Initialize a new <see cref="DeactivatableRepositoryBase{TEntity, TIdentifier}"/>
        /// </summary>
        /// <param name="databaseContext">The database context</param>
        public DeactivatableRepositoryBase(IMaijeDbContext databaseContext) : base(databaseContext)
        {
        }

        /// <summary>
        /// Deactivate an entity
        /// </summary>
        /// <param name="identifier">The entity identifier</param>
        /// <returns></returns>
        public override Task DeleteAsync(TIdentifier identifier)
        {
            var entityToDeactivate = CreateDeleteEntity(identifier);
            entityToDeactivate.IsActive = false;

            var entry = DbSet.Attach(entityToDeactivate);
            entry.Property(e => e.IsActive).IsModified = true;

            return Task.CompletedTask;
        }

        /// <summary>
        /// Gets entity by identifier
        /// </summary>
        /// <param name="identifier">The entity identifier</param>
        /// <param name="trackable"></param>
        /// <returns></returns>
        protected override IQueryable<TEntity> GetByIdQuery(TIdentifier identifier, bool trackable)
        {
            var query = base.GetByIdQuery(identifier, trackable);

            return query.Where(e => e.IsActive);
        }

        /// <summary>
        /// Gets the entities query
        /// </summary>
        /// <param name="trackable">If we need to track the return entities</param>
        /// <returns></returns>
        protected override IQueryable<TEntity> GetItemsQuery(bool trackable)
        {
            var query = base.GetItemsQuery(trackable);

            return query.Where(e => e.IsActive);
        }
    }
}
