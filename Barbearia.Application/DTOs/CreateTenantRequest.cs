using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.DTOs
{
	public class CreateTenantRequest
    {
        public NomeFantasia NomeFantasia { get; set; }
        public string EmailAdmin { get; set; }
        public string SenhaAdmin { get; set; }
        public string NomeAdmin { get; set; }
    }
}
