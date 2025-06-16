using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;

namespace Barbearia.Domain.Factories
{
    public class AgendamentoFactory
    {
        public Agendamento CriarAgendamento(Guid tenantId, Guid servicoId, Guid barbeiroId, DateTime dataHoraNormalizada, string nomeCliente, string telefoneCliente, AgendamentoStatus status)
        {
            return new Agendamento
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                ServicoId = servicoId,
                BarbeiroId = barbeiroId,
                DataHoraAgendada = dataHoraNormalizada,
                ClienteNome = nomeCliente,
                ClienteTelefone = telefoneCliente,
                Status = AgendamentoStatus.Agendado
            };
        }
    }
}
