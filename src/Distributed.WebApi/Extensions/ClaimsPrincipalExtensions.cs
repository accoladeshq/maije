using System;
using System.Security.Claims;

namespace Accolades.Maije.Distributed.WebApi.Extensions
{
    internal static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// gets the user name
        /// </summary>
        /// <param name="claimsPrincipal">The claims principal</param>
        /// <returns></returns>
        public static Guid GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            var userName = claimsPrincipal?.Identity.Name;

            if (string.IsNullOrEmpty(userName))
            {
                return Guid.Empty;
            }

            return Guid.Parse(userName);
        }
    }
}
