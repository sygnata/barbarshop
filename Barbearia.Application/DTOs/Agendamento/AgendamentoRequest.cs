namespace Barbearia.Application.DTOs.Agendamento
{
	public class AgendamentoRequest
    {
        public Guid ClienteId { get; set; } // Podemos substituir por nome e telefone no MVP
        public Guid ServicoId { get; set; }
        public Guid BarbeiroId { get; set; }
        public DateTime DataHora { get; set; }
        public string NomeCliente { get; set; }
        public string TelefoneCliente { get; set; }
    }
}
