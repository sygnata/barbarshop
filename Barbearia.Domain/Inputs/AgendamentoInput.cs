using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Inputs
{
    public class AgendamentoInput
    {
        public TenantId TenantId { get; private set; }
        public ServicoId ServicoId { get; private set; }
        public BarbeiroId BarbeiroId { get; private set; }
        public DateTime DataHoraAgendada { get; private set; }
        public string NomeCliente { get; private set; }
        public Telefone TelefoneCliente { get; private set; }
        public AgendamentoStatus Status { get; private set; }

        public void SetTenantId(TenantId tenantId)
        {
            TenantId = tenantId;
        }

        public AgendamentoInput(
            ServicoId servicoId,
            BarbeiroId barbeiroId,
            DateTime dataHoraAgendada,
            string nomeCliente,
            Telefone telefoneCliente,
            AgendamentoStatus status)
        {
            ServicoId = servicoId;
            BarbeiroId = barbeiroId;
            DataHoraAgendada = dataHoraAgendada;
            NomeCliente = nomeCliente;
            TelefoneCliente = telefoneCliente;
            Status = status;
        }
    }
}
