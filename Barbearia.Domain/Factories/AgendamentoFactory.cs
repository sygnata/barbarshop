using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Inputs;

namespace Barbearia.Domain.Factories
{
    public class AgendamentoFactory
    {
        public Agendamento CriarAgendamento(AgendamentoInput? input)
        {
            return new Agendamento
            {
                Id = Guid.NewGuid(),
                TenantId = input.TenantId,
                ServicoId = input.ServicoId,
                BarbeiroId = input.BarbeiroId,
                DataHoraAgendada = input.DataHoraAgendada,
                ClienteNome = input.NomeCliente,
                ClienteTelefone = input.TelefoneCliente,
                Status = AgendamentoStatus.Agendado
            };
        }
    }
}
