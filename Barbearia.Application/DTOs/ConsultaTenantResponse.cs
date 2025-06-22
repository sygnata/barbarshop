using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.DTOs
{
	public class ConsultaTenantResponse
    {
        public NomeFantasia NomeFantasia { get; set; }//VALUEOBJECT
        public string? LogoUrl { get; set; }
        public string? CorPrimaria { get; set; }
        public string? DominioCustomizado { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
