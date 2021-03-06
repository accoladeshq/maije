﻿using Accolades.Maije.Domain.Contracts;
using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Domain.Services;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Accolades.Maije.Infrastructure
{
    public class DeactivatableMaijeRepository<TEntity, TIdentifier> : MaijeRepository<TEntity, TIdentifier>
        where TEntity : class, IIdentity<TIdentifier>, IDeactivatable
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Initialize a new <see cref="DeactivatableMaijeRepository{TEntity, TIdentifier}"/>
        /// </summary>
        /// <param name="databaseContext">The database context</param>
        public DeactivatableMaijeRepository(IMaijeDbContext databaseContext, IPaginationDomainService paginationDomainService) : base(databaseContext, paginationDomainService)
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
        protected override IQueryable<TEntity> GetItemsQuery(bool trackable, Order order)
        {
            var query = base.GetItemsQuery(trackable, order);

            return query.Where(e => e.IsActive);
        }
    }
}
