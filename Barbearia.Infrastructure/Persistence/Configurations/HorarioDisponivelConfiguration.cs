using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbearia.Infrastructure.Persistence.Configurations
{
    public class HorarioDisponivelConfiguration : IEntityTypeConfiguration<HorarioDisponivel>
    {
        public void Configure(EntityTypeBuilder<HorarioDisponivel> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id);
            builder.Property(a => a.BarbeiroId).HasGuidConversion();
            builder.Property(a => a.TenantId).HasGuidConversion();
            builder.Property(a => a.DiaSemana).IsRequired();
            builder.Property(a => a.HoraInicio).IsRequired();
            builder.Property(a => a.HoraInicio).IsRequired();


			builder.Property(a => a.BarbeiroId)
				.HasConversion(new ValueObjectConverter<BarbeiroId, Guid>(v => new BarbeiroId(v)))
				.HasColumnName("BarbeiroId");
	




        }
    }
}
