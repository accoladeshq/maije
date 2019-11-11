using Accolades.Maije.Crosscutting.Enums;
using Accolades.Maije.Domain.Entities;
using System.Linq;
using System.Linq.Expressions;

namespace Accolades.Maije.Infrastructure.Extensions
{
    public static partial class QueryableExtensions
    {
        /// <summary>
        /// Add pagination parameters to query
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="source">The source query</param>
        /// <param name="paginationRequest">The pagination request</param>
        /// <returns>The request with pagination parameters</returns>
        public static IQueryable<T> AddPagination<T>(this IQueryable<T> source, PaginationRequest paginationRequest)
        {
            return source.Skip(paginationRequest.Offset).Take(paginationRequest.Limit);
        }

        /// <summary>
        /// Apply ordering to query
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="source">The source query</param>
        /// <param name="order">The order</param>
        /// <returns></returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Order order)
        {
            IOrderedQueryable<T> result = null;

            switch (order.Type)
            {
                case OrderType.Desc:
                    result = source.OrderByMemberDescending(order.ColumnName);
                    break;
                case OrderType.Asc:
                    result = source.OrderByMember(order.ColumnName);
                    break;
            }

            return result;
        }

        /// <summary>
        /// Order by member name
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="source">The db set source</param>
        /// <param name="memberPath">The name of the member</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByMember<T>(this IQueryable<T> source, string memberPath)
        {
            return source.OrderByMemberUsing(memberPath, "OrderBy");
        }

        /// <summary>
        /// Order descending by member name
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="source">The db set source</param>
        /// <param name="memberPath">The name of the member</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderByMemberDescending<T>(this IQueryable<T> source, string memberPath)
        {
            return source.OrderByMemberUsing(memberPath, "OrderByDescending");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="memberPath"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenByMember<T>(this IOrderedQueryable<T> source, string memberPath)
        {
            return source.OrderByMemberUsing(memberPath, "ThenBy");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="memberPath"></param>
        /// <returns></returns>
        public static IOrderedQueryable<T> ThenByMemberDescending<T>(this IOrderedQueryable<T> source, string memberPath)
        {
            return source.OrderByMemberUsing(memberPath, "ThenByDescending");
        }

        /// <summary>
        /// Apply the order
        /// </summary>
        /// <typeparam name="T">The entity type</typeparam>
        /// <param name="source">The queryable source</param>
        /// <param name="memberPath">The member path</param>
        /// <param name="method">The method to call</param>
        /// <returns></returns>
        private static IOrderedQueryable<T> OrderByMemberUsing<T>(this IQueryable<T> source, string memberPath, string method)
        {
            var parameter = Expression.Parameter(typeof(T), "item");

            var member = memberPath.Split('.').Aggregate((Expression)parameter, Expression.PropertyOrField);

            var keySelector = Expression.Lambda(member, parameter);

            var methodCall = Expression.Call(typeof(Queryable), method, new[] { parameter.Type, member.Type }, source.Expression, Expression.Quote(keySelector));

            return (IOrderedQueryable<T>)source.Provider.CreateQuery(methodCall);
        }
    }
}
