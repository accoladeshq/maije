using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.Crosscutting.Configurations
{
    public class AuthenticationConfiguration
    {
        /// <summary>
        /// Initialize a new <see cref="AuthenticationConfiguration"/>
        /// </summary>
        /// <param name="authority">The authority url</param>
        /// <param name="clientId">The client identifier</param>
        /// <param name="flow">The authentication flow</param>
        /// <param name="scopes">The needed scopes</param>
        public AuthenticationConfiguration(string authority, string authorizeUrl, string tokenUrl, string clientId, string audience, Dictionary<string, string> scopes, List<string> claims)
        {
            Authority = authority;
            AuthorizeUrl = authorizeUrl;
            TokenUrl = tokenUrl;
            ClientId = clientId;
            Audience = audience;
            Scopes = scopes ?? new Dictionary<string, string>();
            Claims = claims ?? new List<string>();
        }

        /// <summary>
        /// Used by aspnet to inject configuration
        /// </summary>
        public AuthenticationConfiguration()
        {
            // For aspnet core configuration purpose
        }

        /// <summary>
        /// Gets the client identifier
        /// </summary>
        [Required]
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the api audience
        /// </summary>
        [Required]
        public string Audience { get; private set; }

        /// <summary>
        /// Gets the needed scopes for the authentication
        /// </summary>
        [Required]
        public Dictionary<string, string> Scopes { get; private set; }

        /// <summary>
        /// Gets the current user claims to include
        /// </summary>
        public List<string> Claims { get; private set; }

        /// <summary>
        /// Gets the authentication authority url
        /// </summary>
        [Required]
        public string Authority { get; private set; }
        
        /// <summary>
        /// Gets the token url
        /// </summary>
        [Required]
        public string TokenUrl { get; private set; }

        /// <summary>
        /// Gets the authorization url
        /// </summary>
        [Required]
        public string AuthorizeUrl { get; private set; }
    }
}
