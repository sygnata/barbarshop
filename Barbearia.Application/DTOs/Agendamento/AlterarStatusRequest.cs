namespace Barbearia.Application.DTOs.Status
{
	public class AlterarStatusRequest
    {
        public Guid AgendamentoId { get; set; }
        public int NovoStatus { get; set; } // Enviado pelo Front como inteiro
    }
}
