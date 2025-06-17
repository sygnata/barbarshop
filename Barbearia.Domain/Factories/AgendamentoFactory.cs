using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Inputs;

namespace Barbearia.Domain.Factories
{
    public class AgendamentoFactory
    {
        //public Agendamento CriarAgendamento(Guid tenantId, Guid servicoId, Guid barbeiroId, DateTime dataHoraNormalizada, string nomeCliente, string telefoneCliente, AgendamentoStatus status)
        //{
        //    return new Agendamento
        //    {
        //        Id = Guid.NewGuid(),
        //        TenantId = tenantId,
        //        ServicoId = servicoId,
        //        BarbeiroId = barbeiroId,
        //        DataHoraAgendada = dataHoraNormalizada,
        //        ClienteNome = nomeCliente,
        //        ClienteTelefone = telefoneCliente,
        //        Status = AgendamentoStatus.Agendado
        //    };
        //}
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
