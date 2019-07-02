using Accolades.Maije.Crosscutting.Enums;
using Accolades.Maije.Crosscutting.Exceptions;
using System;

namespace Accolades.Maije.Domain.Entities
{
    public class Order
    {
        /// <summary>
        /// Initialize a new order
        /// </summary>
        /// <param name="orderType">The order type</param>
        /// <param name="columnName">The column name</param>
        public Order(OrderType type, string columnName)
        {
            Type = type;
            ColumnName = columnName ?? throw new BusinessException("You must provide a column name to order the query result", 102);
        }

        /// <summary>
        /// Gets the order type
        /// </summary>
        public OrderType Type { get; }

        /// <summary>
        /// Gets the column name
        /// </summary>
        public string ColumnName { get; }
    }
}
