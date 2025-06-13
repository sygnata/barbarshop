using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.DTOs.Status;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Interfaces
{
    public interface IAgendamentoService
    {
        AgendamentoResponse Agendar(Guid tenantId, AgendamentoRequest request);
        IEnumerable<AgendamentoResponse> ListarAgendamentos(Guid tenantId);
        void AlterarStatus(Guid tenantId, AlterarStatusRequest request);
    }
}
