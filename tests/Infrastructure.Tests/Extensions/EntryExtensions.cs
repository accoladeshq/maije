using Accolades.Maije.Domain.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Infrastructure.Tests.Extensions
{
    internal static class EntryExtensions
    {
        /// <summary>
        /// Gets an <see cref="EntityEntry"/> function of an identifier
        /// </summary>
        /// <typeparam name="TIdentifier">The entity identifier type</typeparam>
        /// <param name="entries"></param>
        /// <param name="id">The entity identifier</param>
        /// <returns></returns>
        public static EntityEntry FirstById<TIdentifier>(this IEnumerable<EntityEntry> entries, TIdentifier id)
            where TIdentifier : IEquatable<TIdentifier>
        {
            var entry = entries.First(e => ((IIdentity<TIdentifier>)e.Entity).Id.Equals(id));

            return entry;
        }
    }
}
