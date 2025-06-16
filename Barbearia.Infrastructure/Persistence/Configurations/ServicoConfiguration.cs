using Barbearia.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence.ValueConverters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Barbearia.Infrastructure.Persistence.Configurations
{
	internal class ServicoConfiguration : IEntityTypeConfiguration<Servico>
	{
		public void Configure(EntityTypeBuilder<Servico> builder)
		{
			builder.HasKey(a => a.Id);
			builder.Property(a => a.Id);
			builder.Property(a => a.TenantId).HasGuidConversion();
			builder.Property(a => a.Nome).IsRequired();
			builder.Property(a => a.Descricao).IsRequired();
			builder.Property(a => a.DuracaoMinutos).IsRequired();
			builder.Property(a => a.Preco).IsRequired();


			builder.Property(a => a.TenantId)
				.HasConversion(new ValueObjectConverter<TenantId, Guid>(v => new TenantId(v)))
				.HasColumnName("TenantId");
			builder.Property(a => a.Nome)
				.HasConversion(new ValueObjectConverter<NomeServico, string>(v => new NomeServico(v)))
				.HasColumnName("Nome");
			builder.Property(a => a.Descricao)
				.HasConversion(new ValueObjectConverter<DescricaoServico, string>(v => new DescricaoServico(v)))
				.HasColumnName("Descricao");




		}
	}
}
