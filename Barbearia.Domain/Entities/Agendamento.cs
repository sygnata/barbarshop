using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Entities
{
	public class Agendamento
    {
        public Guid Id { get; set; }
        public TenantId TenantId { get; set; }
        public BarbeiroId BarbeiroId { get; set; }
        public ServicoId ServicoId { get; set; }
        public DateTime DataHoraAgendada { get; set; }
        public string ClienteNome { get; set; }
        public Telefone ClienteTelefone { get; set; }
        public AgendamentoStatus Status { get; set; } // AGENDADO, CANCELADO, CONCLUIDO
    }
}
