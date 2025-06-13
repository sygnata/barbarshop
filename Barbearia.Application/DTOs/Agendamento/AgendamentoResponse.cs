namespace Barbearia.Application.DTOs.Agendamento
{
	public class AgendamentoResponse
    {
        public Guid Id { get; set; }
        public Guid ServicoId { get; set; }
        public Guid BarbeiroId { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
    }
}
