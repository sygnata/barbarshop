namespace Barbearia.Application.DTOs.Agendamento
{
	public class DisponibilidadeRequest
    {
        public Guid BarbeiroId { get; set; }
        public Guid ServicoId { get; set; }
        public DateTime Data { get; set; }
    }
}
