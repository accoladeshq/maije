using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;

namespace Accolades.Maije.Crosscutting.Configurations
{
    public class MaijeConfiguration
    {
        /// <summary>
        /// Intialize a new <see cref="ApiConfiguration"/>
        /// </summary>
        /// <param name="baseUrl">The base url of the swagger api</param>
        /// <param name="title">The title of the swagger api</param>
        /// <param name="authConfiguration">The authentication configuration</param>
        public MaijeConfiguration(string baseUrl, string title, IEnumerable<string> cors, bool swaggerEnabled, AuthenticationConfiguration authConfiguration = null)
        {
            BaseUrl = baseUrl;
            Title = title;
            DocumentationEnabled = swaggerEnabled;
            Cors = cors.ToList();
            Authentication = authConfiguration;
        }

        /// <summary>
        /// Used by aspnet core configuration
        /// </summary>
        public MaijeConfiguration()
        {

        }

        /// <summary>
        /// Gets value indicating if swagger enabled
        /// </summary>
        public bool DocumentationEnabled { get; private set; }

        /// <summary>
        /// Gets the swagger base url
        /// </summary>
        [Required]
        public string BaseUrl { get; private set; }

        /// <summary>
        /// Gets value indicating if OAuth2 is enabled
        /// </summary>
        public bool OAuth2Enabled => Authentication != null;

        /// <summary>
        /// Gets the api title
        /// </summary>
        [Required]
        public string Title { get; private set; }

        /// <summary>
        /// Gets the authentication configuration
        /// </summary>
        public AuthenticationConfiguration Authentication { get; private set; }

        /// <summary>
        /// Gets ressource list who can access the api
        /// </summary>
        [Required]
        public List<string> Cors { get; private set; } = new List<string>();

        /// <summary>
        /// Gets the name claim type
        /// </summary>
        public string NameClaimType
        {
            get { return ClaimTypes.NameIdentifier; }
        }
    }
}
