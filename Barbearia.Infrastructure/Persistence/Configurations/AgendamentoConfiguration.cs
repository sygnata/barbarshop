using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Barbearia.Infrastructure.Persistence.Configurations
{
	public class AgendamentoConfiguration : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id);
            builder.Property(a => a.TenantId).HasGuidConversion();
            builder.Property(a => a.ServicoId).HasGuidConversion();
            builder.Property(a => a.BarbeiroId).HasGuidConversion();
            builder.Property(a => a.ClienteNome).IsRequired();
            builder.Property(a => a.ClienteTelefone).IsRequired();
            builder.Property(a => a.DataHoraAgendada).IsRequired();
            builder.Property(a => a.Status).IsRequired();

            builder.Property(a => a.ClienteTelefone)
                .HasConversion(
                    v => v.Value,  // Quando salva no banco
                    v => new Telefone(v) // Quando lê do banco
                ).HasColumnName("ClienteTelefone");

            builder.Property(a => a.ClienteTelefone)
                .HasConversion(new ValueObjectConverter<Telefone, string>(v => new Telefone(v)))
                .HasColumnName("ClienteTelefone");
        }
    }
}
