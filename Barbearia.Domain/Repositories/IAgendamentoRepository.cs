using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IAgendamentoRepository
    {
        void Adicionar(Agendamento agendamento);
        IEnumerable<Agendamento> ListarPorTenant(Guid tenantId);
        bool ExisteConflito(Guid tenantId, Guid barbeiroId, DateTime dataHoraInicio, DateTime dataHoraFim);
        bool ClientePossuiDuplicado(Guid tenantId, string telefoneCliente, DateTime dataHora);
        void Salvar();
        Agendamento? ObterPorId(Guid agendamentoId, Guid tenantId);
        IEnumerable<(DateTime DataHoraAgendada, int DuracaoMinutos)> ObterAgendamentosComServico(Guid tenantId, Guid barbeiroId, DateTime dataReferencia);
    }
}
