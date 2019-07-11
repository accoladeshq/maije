using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Accolades.Maije.Distributed.Documentation.Filters
{
    // Used to correct issue with lower case of the ApiExplorer lib
    // https://github.com/domaindrivendev/Swashbuckle/issues/834
    internal class LowercaseDocumentFilter : IDocumentFilter
    {
        /// <summary>
        /// Apply the filter to the document
        /// </summary>
        /// <param name="swaggerDoc">The document to apply filter</param>
        /// <param name="context">The document context</param>
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            swaggerDoc.Paths = swaggerDoc.Paths.ToDictionary(entry => LowercaseEverythingButParameters(entry.Key), entry => entry.Value);
        }

        /// <summary>
        /// Lower case everything except parameters
        /// </summary>
        /// <param name="key">The key to lower</param>
        /// <returns></returns>
        private string LowercaseEverythingButParameters(string key)
        {
            return string.Join("/", key.Split('/').Select(x => x.Contains("{") ? x : x.ToLower()));
        }

    }
}
