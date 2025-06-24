using Barbearia.Domain.ValueObjects;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Barbearia.Infrastructure.Persistence.ValueConverters
{
	public class ValueObjectJsonConverter<TValueObject, TPrimitive> : JsonConverter<TValueObject>
        where TValueObject : ValueObject<TPrimitive>
    {
        public override TValueObject Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var primitiveValue = JsonSerializer.Deserialize<TPrimitive>(ref reader, options);
            return (TValueObject)Activator.CreateInstance(typeof(TValueObject), primitiveValue)!;
        }

        public override void Write(Utf8JsonWriter writer, TValueObject value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Value, options);
        }
    }
}
