using Accolades.Maije.Crosscutting.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Accolades.Maije.Crosscutting.Configurations
{
    public class ClientConfiguration
    {
        /// <summary>
        /// Initialize default <see cref="ClientConfiguration"/>
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="flow"></param>
        /// <param name="scopes"></param>
        /// <param name="claims"></param>
        public ClientConfiguration(string clientId, string audience, OAuth2Flow flow, Dictionary<string, string> scopes, List<string> claims)
        {
            ClientId = clientId;
            Audience = audience;
            Flow = flow;
            Scopes = scopes ?? new Dictionary<string, string>();
            Claims = claims ?? new List<string>();

        }

        /// <summary>
        /// Initialize a new <see cref="ClientConfiguration"/>
        /// </summary>
        /// <param name="clientId">The client identifier</param>
        /// <param name="clientSecret">The client secret</param>
        public ClientConfiguration(string clientId, string clientSecret)
        {
            Flow = OAuth2Flow.Code;
            ClientId = clientId;
            ClientSecret = clientSecret;
            Scopes = new Dictionary<string, string>();
            Claims = new List<string>();
        }

        public ClientConfiguration()
        {
            // For Asp.net core configuration purpose
        }

        /// <summary>
        /// Gets the client identifier
        /// </summary>
        [Required]
        public string ClientId { get; private set; }

        /// <summary>
        /// Gets the api audience
        /// </summary>
        public string Audience { get; private set; }

        /// <summary>
        /// Gets the client secret
        /// </summary>
        public string ClientSecret { get; private set; }

        /// <summary>
        /// Gets the authentication flow
        /// </summary>
        [Required]
        public OAuth2Flow Flow { get; private set; }

        /// <summary>
        /// Gets the needed scopes for the authentication
        /// </summary>
        [Required]
        public Dictionary<string, string> Scopes { get; private set; }

        /// <summary>
        /// Gets the current user claims to include
        /// </summary>
        public List<string> Claims { get; private set; }
    }
}
