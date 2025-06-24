using Barbearia.Domain.ValueObjects;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Barbearia.Infrastructure.Swagger
{
	public class ValueObjectSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var type = context.Type;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(ValueObject<>))
            {
                // Não aplica a própria base ValueObject<>
                return;
            }

            // Verifica se o type herda de ValueObject<TPrimitive>
            var baseType = type.BaseType;
            if (baseType == null || !baseType.IsGenericType) return;

            if (baseType.GetGenericTypeDefinition() == typeof(ValueObject<>))
            {
                var primitiveType = baseType.GetGenericArguments()[0];
                schema.Type = MapPrimitiveTypeToSwaggerType(primitiveType);
                schema.Format = GetSwaggerFormat(primitiveType);
                schema.Properties.Clear();
            }
        }

        private string MapPrimitiveTypeToSwaggerType(Type primitiveType)
        {
            if (primitiveType == typeof(Guid)) return "string";
            if (primitiveType == typeof(string)) return "string";
            if (primitiveType == typeof(int)) return "integer";
            if (primitiveType == typeof(decimal)) return "number";
            if (primitiveType == typeof(double)) return "number";
            if (primitiveType == typeof(DateTime)) return "string";
            return "string"; // fallback
        }

        private string? GetSwaggerFormat(Type primitiveType)
        {
            if (primitiveType == typeof(Guid)) return "uuid";
            if (primitiveType == typeof(DateTime)) return "date-time";
            if (primitiveType == typeof(int)) return "int32";
            if (primitiveType == typeof(long)) return "int64";
            if (primitiveType == typeof(decimal)) return "decimal";
            return null;
        }
    }
}
