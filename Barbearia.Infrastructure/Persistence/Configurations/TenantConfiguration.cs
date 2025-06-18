using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Barbearia.Infrastructure.Persistence.Configurations
{
	internal class TenantConfiguration : IEntityTypeConfiguration<Tenant>
	{
		public void Configure(EntityTypeBuilder<Tenant> builder)
		{
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id);
			builder.Property(a => a.NomeFantasia).IsRequired();
			builder.Property(a => a.LogoUrl).IsRequired(false);
			builder.Property(a => a.CorPrimaria).IsRequired(false);
			builder.Property(a => a.DominioCustomizado).IsRequired(false);
			builder.Property(a => a.DataCriacao).IsRequired();


			builder.Property(a => a.NomeFantasia)
				.HasConversion(new ValueObjectConverter<NomeFantasia, string>(v => new NomeFantasia(v)))
				.HasColumnName("NomeFantasia");





		}
	}
}
