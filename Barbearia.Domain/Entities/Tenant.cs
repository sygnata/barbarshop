using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Entities
{
	public class Tenant
    {
        public Guid Id { get; set; }
        public NomeFantasia NomeFantasia { get; set; }//VALUEOBJECT
        public string? LogoUrl { get; set; }
        public string? CorPrimaria { get; set; }
        public string? DominioCustomizado { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
