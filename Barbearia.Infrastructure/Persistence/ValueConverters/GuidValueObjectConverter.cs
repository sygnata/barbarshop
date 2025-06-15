using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barbearia.Infrastructure.Persistence.ValueConverters
{
	public class GuidValueObjectConverter<TValueObject> : ValueConverter<TValueObject, Guid>
    where TValueObject : class
    {
        public GuidValueObjectConverter(Func<Guid, TValueObject> factory)
            : base(
                vo => (Guid)vo!.GetType().GetProperty("Value")!.GetValue(vo)!,
                guid => factory(guid))
        { }
    }
}
