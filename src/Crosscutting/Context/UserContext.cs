using System;
using System.Collections.Generic;

namespace Accolades.Maije.Crosscutting.Context
{

    public class UserContext : IUserContext
    {
        /// <summary>
        /// Initialize a new <see cref="UserContext"/>
        /// </summary>
        /// <param name="userId"></param>
        public UserContext(Guid userId, Dictionary<string, string> userClaims)
        {
            UserId = userId;
            Claims = userClaims ?? new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the current user
        /// </summary>
        public Guid UserId { get; private set; }

        /// <summary>
        /// Gets value indicating if the current user is authenticated
        /// </summary>
        public bool IsAuthenticated
        {
            get { return UserId != Guid.Empty; }
        }

        /// <summary>
        /// Gets 
        /// </summary>
        public IReadOnlyDictionary<string, string> Claims { get; private set; }
    }
}
