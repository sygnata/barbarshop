using Barbearia.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barbearia.Infrastructure.Persistence.ValueConverters
{
	public class ValueObjectConverter<TValueObject, TPrimitive> : ValueConverter<TValueObject, TPrimitive>
        where TValueObject : ValueObject<TPrimitive>
    {
        public ValueObjectConverter(
            Func<TPrimitive, TValueObject> factory
        ) : base(vo => vo.Value, v => factory(v)) { }
    }
}
