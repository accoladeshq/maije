using System;

namespace Accolades.Maije.Crosscutting.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Check if the current type is a primitive
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsPrimitive(this Type type)
        {
            if (type == typeof(string)) return true;

            return (type.IsValueType & type.IsPrimitive);
        }
    }
}
