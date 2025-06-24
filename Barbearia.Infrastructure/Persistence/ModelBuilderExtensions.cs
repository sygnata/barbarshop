using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbearia.Infrastructure.Persistence
{
	public static class ModelBuilderExtensions
    {
        public static PropertyBuilder<TValueObject> HasGuidConversion<TValueObject>(this PropertyBuilder<TValueObject> builder)
            where TValueObject : class
        {
            builder.HasConversion(new GuidValueObjectConverter<TValueObject>(guid =>
                (TValueObject)Activator.CreateInstance(typeof(TValueObject), guid)!));

            return builder;
        }
    }
}
