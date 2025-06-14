using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.DTOs.Status;
using Barbearia.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Interfaces
{
    public interface IAgendamentoService
    {
        //AgendamentoResponse Agendar(Guid tenantId, AgendamentoRequest request);
        void Agendar(Guid tenantId, AgendamentoRequest agendamento);
        IEnumerable<AgendamentoResponse> ListarAgendamentos(Guid tenantId);
        void AlterarStatus(Guid tenantId, AlterarStatusRequest request);
        IEnumerable<DateTime> ListarDisponibilidade(Guid tenantId, Guid barbeiroId, Guid servicoId, DateTime dataReferencia);
    }
}
