using Accolades.Maije.Crosscutting.Attributes;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Reflection;

namespace Accolades.Maije.Distributed.Documentation.Filters
{
    internal class SubtypeOfAttributeFilter : ISchemaFilter
    {
        /// <summary>
        /// Apply schema filter to filter context
        /// </summary>
        /// <param name="model">The schema model</param>
        /// <param name="context">The filter context</param>
        public void Apply(Schema model, SchemaFilterContext context)
        {
            var subTypeAttrib = context
                .SystemType
                .GetTypeInfo()
                .GetCustomAttribute<SubtypeOfAttribute>();

            if (subTypeAttrib != null)
            {
                if (model.AllOf == null)
                {
                    model.AllOf = new List<Schema>();
                }

                var schemaRef = context.SchemaRegistry.GetOrRegister(context.SystemType.BaseType);
                model.AllOf.Add(schemaRef);

                //Replace with a better method of getting full schema given a type
                var actualSchema = context
                    .SchemaRegistry
                    .Definitions[schemaRef.Ref.Replace("#/definitions/", "")];

                //Remove any properties defined by the parent from the child
                foreach (string propName in actualSchema.Properties.Keys)
                {
                    model.Properties.Remove(propName);
                }

                //Move the remaining properties to the "allOf" set
                model.AllOf.Add(new Schema()
                {
                    Properties = model.Properties
                });
                model.Properties = new Dictionary<string, Schema>();
            }
        }
    }
}
