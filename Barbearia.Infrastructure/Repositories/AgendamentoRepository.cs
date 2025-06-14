using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
    public class AgendamentoRepository : IAgendamentoRepository
    {
        private readonly BarbeariaDbContext _context;

        public AgendamentoRepository(BarbeariaDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Agendamento agendamento)
        {
            _context.Agendamentos.Add(agendamento);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public IEnumerable<Agendamento> ListarPorTenant(Guid tenantId)
        {
            return _context.Agendamentos.Where(a => a.TenantId == tenantId).ToList();
        }

        public bool ClientePossuiDuplicado(Guid tenantId, string telefoneCliente, DateTime dataHora)
        {
            return _context.Agendamentos.Any(a =>
                a.TenantId == tenantId &&
                a.ClienteTelefone == telefoneCliente &&
                a.DataHoraAgendada == dataHora &&
                a.Status == AgendamentoStatus.Agendado);
        }

        public bool ExisteConflito(Guid tenantId, Guid barbeiroId, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            return (from a in _context.Agendamentos
                    join s in _context.Servicos on a.ServicoId equals s.Id
                    where a.TenantId == tenantId
                          && a.BarbeiroId == barbeiroId
                          && a.Status == AgendamentoStatus.Agendado
                    select new
                    {
                        a.DataHoraAgendada,
                        Duracao = s.DuracaoMinutos
                    })
                .Any(a =>
                    dataHoraInicio < a.DataHoraAgendada.AddMinutes(a.Duracao)
                    && dataHoraFim > a.DataHoraAgendada);
        }

        public Agendamento? BuscarDisponivel(Guid agendamentoId, Guid tenantId)
        {
            return _context.Agendamentos.FirstOrDefault(a => a.Id == agendamentoId && a.TenantId == tenantId);

        }
        public IEnumerable<(DateTime DataHoraAgendada, int DuracaoMinutos)> ObterAgendamentosComServico(Guid tenantId, Guid barbeiroId, DateTime dataReferencia)
        {
            return (from a in _context.Agendamentos
                    join s in _context.Servicos on a.ServicoId equals s.Id
                    where a.TenantId == tenantId
                          && a.BarbeiroId == barbeiroId
                          && a.Status == AgendamentoStatus.Agendado
                          && a.DataHoraAgendada.Date == dataReferencia.Date
                    select new ValueTuple<DateTime, int>(a.DataHoraAgendada, s.DuracaoMinutos))
                .ToList();
        }
    }


}
