using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Entities
{
	public class Barbeiro
    {
        public BarbeiroId Id { get; set; }
        public TenantId TenantId { get; set; }
        public NomeBarbeiro Nome { get; set; }
        public string? FotoUrl { get; set; }
        public DescricaoBarbeiro? Descricao { get; set; }
    }
}
