using System;
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
        public AuthenticationConfiguration(string authority, string realm, ClientConfiguration frontClient, ClientConfiguration backOfficeClient)
        {
            Authority = authority;
            Realm = realm;
            FrontOfficeClient = frontClient;
            BackOfficeClient = backOfficeClient;
        }

        /// <summary>
        /// Used by aspnet to inject configuration
        /// </summary>
        public AuthenticationConfiguration()
        {
            // For aspnet core configuration purpose
        }

        /// <summary>
        /// Gets the authentication realm
        /// </summary>
        public string Realm { get; private set; }

        /// <summary>
        /// Gets the authentication authority url
        /// </summary>
        [Required]
        public string Authority { get; private set; }

        /// <summary>
        /// Gets the authentication url
        /// </summary>
        public string AuthenticationUrl
        {
            get
            {
                return new Uri(new Uri(Authority), $"").AbsoluteUri;
            }
        }

        /// <summary>
        /// Gets the token url
        /// </summary>
        public string TokenUrl
        {
            get { return new Uri(new Uri(AuthenticationUrl), "connect/token").AbsoluteUri; }
        }

        /// <summary>
        /// Gets the authorization url
        /// </summary>
        public string AuthorizationUrl
        {
            get { return new Uri(new Uri(AuthenticationUrl), "connect/authorize").AbsoluteUri; }
        }

        /// <summary>
        /// Gets the back office client information
        /// </summary>
        public ClientConfiguration BackOfficeClient { get; private set; }

        /// <summary>
        /// Gets the front office client configuration
        /// </summary>
        public ClientConfiguration FrontOfficeClient { get; private set; }
    }
}
