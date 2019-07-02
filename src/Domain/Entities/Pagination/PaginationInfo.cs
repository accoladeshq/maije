using Accolades.Maije.Crosscutting.Exceptions;

namespace Accolades.Maije.Domain.Entities
{
    public class PaginationInfo
    {
        /// <summary>
        /// Initialize a <see cref="PaginationInfo"/>
        /// </summary>
        /// <param name="offset">The offset</param>
        /// <param name="limit">The limit</param>
        public PaginationInfo(int offset, int limit)
        {
            Offset = offset >= 0 ? offset : throw new BusinessException($"{nameof(offset)} cannot be lesser than 1", 100);
            Limit = limit > 0 ? limit : throw new BusinessException($"{nameof(limit)} cannot be lesser than 1", 101);
        }

        /// <summary>
        /// Gets the pagination offset
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the pagination limit
        /// </summary>
        public int Limit { get; }
    }
}
