using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
    public interface IAgendamentoRepository
    {
        void Adicionar(Agendamento agendamento);
        IEnumerable<Agendamento> ListarPorTenant(TenantId tenantId);
        bool ExisteConflito(TenantId tenantId, BarbeiroId barbeiroId, DateTime dataHoraInicio, DateTime dataHoraFim);
        bool ClientePossuiDuplicado(TenantId tenantId, string telefoneCliente, DateTime dataHora);
        void Salvar();
        Agendamento? ObterPorId(Guid agendamentoId, TenantId tenantId);
        IEnumerable<(DateTime DataHoraAgendada, int DuracaoMinutos)> ObterAgendamentosComServico(TenantId tenantId, BarbeiroId barbeiroId, DateTime dataReferencia);
    }
}
