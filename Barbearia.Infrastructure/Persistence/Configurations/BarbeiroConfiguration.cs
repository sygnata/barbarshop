using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Barbearia.Domain.ValueObjects;
using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Barbearia.Infrastructure.Persistence.Configurations
{
	public class BarbeiroConfiguration : IEntityTypeConfiguration<Barbeiro>
    {
        public void Configure(EntityTypeBuilder<Barbeiro> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id);
            builder.Property(a => a.TenantId).HasGuidConversion();
            builder.Property(a => a.Nome).IsRequired();
            builder.Property(a => a.FotoUrl).IsRequired(false);
            builder.Property(a => a.Descricao).IsRequired(false);


            builder.Property(a => a.Id)
                .HasConversion(new ValueObjectConverter<BarbeiroId, Guid>(v => new BarbeiroId(v)))
                .HasColumnName("Id");
            //builder.Property(a => a.TenantId)
            //    .HasConversion(new ValueObjectConverter<TenantId, Guid>(v => new TenantId(v)))
            //    .HasColumnName("TenantId");
            builder.Property(a => a.Nome)
                .HasConversion(new ValueObjectConverter<NomeBarbeiro, string>(v => new NomeBarbeiro(v)))
                .HasColumnName("Nome");
            builder.Property(a => a.Descricao)
                .HasConversion(new ValueObjectConverter<DescricaoBarbeiro, string>(v => new DescricaoBarbeiro(v!)) as ValueConverter<DescricaoBarbeiro?, string?>)
                .HasColumnName("Descricao");




        }
    }
}
