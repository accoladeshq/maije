using System;

namespace Accolades.Maije.Domain.Entities
{
    public interface IIdentity<T> where T : IEquatable<T> // we use IEquatable because struct cause an issue with string
    {
        /// <summary>
        /// Gets the entity identifier
        /// </summary>
        T Id { get; }
    }
}
