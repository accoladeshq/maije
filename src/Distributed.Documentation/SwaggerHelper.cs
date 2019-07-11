using Accolades.Maije.Crosscutting.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;
using System.Reflection;

namespace Accolades.Maije.Distributed.Documentation
{
    internal static class SwaggerHelper
    {
        /// <summary>
        /// Gets the xml comments file path
        /// </summary>
        /// <returns></returns>
        public static string GetXmlCommentsFilePath()
        {
            var basePath = AppContext.BaseDirectory;

            var fileName = Assembly.GetEntryAssembly().GetName().Name + ".xml";

            return Path.Combine(basePath, fileName);
        }

        /// <summary>
        /// Create info for an api version
        /// </summary>
        /// <param name="description">The api description</param>
        /// <returns>The info about this api</returns>
        public static Info CreateInfoForApiVersion(ApiVersionDescription description, MaijeConfiguration apiConfiguration)
        {
            var info = new Info()
            {
                Title = apiConfiguration.Title,
                Version = description.ApiVersion.ToString(),
            };

            if (description.IsDeprecated)
            {
                info.Description += " This API version has been deprecated.";
            }

            return info;
        }

        /// <summary>
        /// Generate the custom schema id
        /// </summary>
        /// <param name="arg">The type use to generate custom schema</param>
        /// <returns>The custom schema name</returns>
        public static string GetCustomSchemaId(Type arg)
        {
            var friendlyId = arg.FriendlyId(false);

            return friendlyId.Replace("Dto", string.Empty);
        }
    }
}
