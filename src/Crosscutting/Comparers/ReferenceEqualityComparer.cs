using System.Collections.Generic;

namespace Accolades.Maije.Crosscutting.Comparers
{
    public class ReferenceEqualityComparer : EqualityComparer<object>
    {
        /// <summary>
        /// Check if object have the same references
        /// </summary>
        /// <param name="x">The first object</param>
        /// <param name="y">The second object</param>
        /// <returns></returns>
        public override bool Equals(object x, object y)
        {
            return ReferenceEquals(x, y);
        }

        /// <summary>
        /// Gets object hash code
        /// </summary>
        /// <param name="obj">The object we want the hash code</param>
        /// <returns></returns>
        public override int GetHashCode(object obj)
        {
            if (obj == null) return 0;
            return obj.GetHashCode();
        }
    }
}
