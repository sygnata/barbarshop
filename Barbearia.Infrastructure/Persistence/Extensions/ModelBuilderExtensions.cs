

using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Linq.Expressions;

namespace Barbearia.Infrastructure.Persistence.Extensions
{
	public static class ModelBuilderExtensions
    {
        public static void ApplyValueObjectConversions(this ModelBuilder modelBuilder)
        {
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var propertyType = property.ClrType;

                    if (!propertyType.IsValueType && propertyType.IsGenericType)
                        continue;

                    if (propertyType.BaseType == null)
                        continue;

                    // Verifica se é ValueObject<>
                    if (IsValueObject(propertyType))
                    {
                        ApplyConverter(property, propertyType);
                    }
                }
            }
        }

        private static bool IsValueObject(Type type)
        {
            return type.BaseType != null
                && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(ValueObject<>);
        }

        private static void ApplyConverter(IMutableProperty property, Type valueObjectType)
        {
            var primitiveType = valueObjectType.BaseType!.GetGenericArguments()[0];

            var converterType = typeof(ValueObjectConverter<,>).MakeGenericType(valueObjectType, primitiveType);

            var factoryConstructor = valueObjectType.GetConstructor(new[] { primitiveType });

            if (factoryConstructor == null)
                throw new InvalidOperationException($"Não foi encontrado um construtor público para {valueObjectType.Name} com parâmetro {primitiveType.Name}");

            // Cria delegate factory via lambda compilado
            var param = Expression.Parameter(primitiveType, "value");
            var constructorCall = Expression.New(factoryConstructor, param);
            var lambda = Expression.Lambda(constructorCall, param);
            var factoryDelegate = lambda.Compile();

            // Instancia o ValueConverter com o delegate
            var converter = Activator.CreateInstance(converterType, factoryDelegate);

            property.SetValueConverter((ValueConverter)converter);
        }
    }
}
