using System;
using System.Collections.Generic;

namespace Accolades.Maije.Crosscutting.Context
{
    public interface IUserContext
    {
        /// <summary>
        /// Gets the current user
        /// </summary>
        Guid UserId { get; }

        /// <summary>
        /// Gets value indicating if the current user is authenticated
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Gets 
        /// </summary>
        IReadOnlyDictionary<string, string> Claims { get; }
    }
}
