using System;

namespace Accolades.Maije.Domain.Entities
{
    public class PaginationResultInfo : PaginationInfo
    {
        /// <summary>
        /// Initialize a <see cref="PaginationResultInfo"/>
        /// </summary>
        /// <param name="offset">The offset</param>
        /// <param name="limit">The limit</param>
        /// <param name="total">Total of items</param>
        public PaginationResultInfo(int offset, int limit, int total) : base(offset, limit)
        {
            Total = total >= 0 ? total : throw new ArgumentOutOfRangeException($"{nameof(total)} cannot be negative");
        }

        /// <summary>
        /// Gets the total number of items
        /// </summary>
        public int Total { get; }
    }
}
