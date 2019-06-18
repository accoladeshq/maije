using Accolades.Maije.Domain.Entities;
using Accolades.Maije.Domain.Entities.Commons;
using System.Collections.Generic;
using System.Linq;

namespace Accolades.Maije.Infrastructure.Tests.Extensions
{
    internal static class ListExtensions
    {
        public static int GetExistingId<TEntity>(this IList<TEntity> dataSet)
            where TEntity : IIdentity<int>
        {
            return dataSet.Last().Id;
        }

        public static int GetNonExistingId<TEntity>(this IList<TEntity> dataSet)
            where TEntity : IIdentity<int>
        {
            return dataSet.Select(e => e.Id).Max() + 1;
        }

        public static int GetDeactivatedId<TEntity>(this IList<TEntity> dataSet)
            where TEntity : IDeactivatable, IIdentity<int>
        {
            return dataSet.Where(e => !e.IsActive).Last().Id;
        }

        public static int ActivateCount<TEntity>(this IList<TEntity> dataSet)
            where TEntity : IDeactivatable
        {
            return dataSet.Count(e => e.IsActive);
        }

        public static int GetActivatedId<TEntity>(this IList<TEntity> dataSet)
            where TEntity : IIdentity<int>, IDeactivatable
        {
            return dataSet.Where(e => e.IsActive).Last().Id;
        }
    }
}
