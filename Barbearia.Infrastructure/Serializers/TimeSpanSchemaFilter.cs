using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Barbearia.Infrastructure.Serializers
{
    public class TimeSpanSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(TimeSpan) || context.Type == typeof(TimeSpan?))
            {
                schema.Type = "string";
                schema.Format = "timespan";
                schema.Example = new OpenApiString("08:00:00");
            }
        }
    }
}
