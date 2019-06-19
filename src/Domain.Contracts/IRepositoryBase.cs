﻿using Accolades.Maije.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Accolades.Maije.Domain.Contracts
{
    public interface IRepositoryBase
    {
    }

    public interface IRepositoryBase<TEntity, TIdentifier> : IRepositoryBase
        where TEntity : class, IIdentity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Gets an item function of an identifier
        /// </summary>
        /// <param name="id">The identifier of the item</param>
        /// <returns>The item or null if not found</returns>
        Task<TEntity> GetItemByIdAsync(TIdentifier id);

        /// <summary>
        /// Gets an item function of an identifier
        /// </summary>
        /// <param name="identifier">The identifier</param>
        /// <param name="trackable">If we need to track changes for this entity</param>
        /// <returns></returns>
        Task<TEntity> GetItemByIdAsync(TIdentifier identifier, bool trackable);

        /// <summary>
        /// Gets items
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetItemsAsync();

        /// <summary>
        /// Create an entity asynchronously
        /// </summary>
        /// <param name="entityToCreate">The entity to create</param>
        /// <returns></returns>
        Task<TIdentifier> AddAsync(TEntity entityToCreate);

        /// <summary>
        /// Updates an entity asynchronously.
        /// </summary>
        /// <param name="entityToUpdate">The entity to update.</param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entityToUpdate);

        /// <summary>
        /// Delete an entity asynchronously
        /// </summary>
        /// <param name="identifier">The identifier to delete</param>
        /// <returns></returns>
        Task DeleteAsync(TIdentifier identifier);
    }
}
