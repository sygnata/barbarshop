using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
    public class AgendamentoRepository : BaseRepository<Agendamento>, IAgendamentoRepository
    {

        public AgendamentoRepository(BarbeariaDbContext context) : base(context) { }

        public IEnumerable<Agendamento> ListarPorTenant(TenantId tenantId)
        {
            return _context.Agendamentos.Where(a => a.TenantId == tenantId).ToList();
        }

        public bool ClientePossuiDuplicado(TenantId tenantId, string telefoneCliente, DateTime dataHora)
        {
            return _context.Agendamentos.Any(a =>
                a.TenantId == tenantId &&
                a.ClienteTelefone == telefoneCliente &&
                a.DataHoraAgendada == dataHora &&
                a.Status == AgendamentoStatus.Agendado);
        }

        public bool ExisteConflito(TenantId tenantId, BarbeiroId barbeiroId, DateTime dataHoraInicio, DateTime dataHoraFim)
        {
            var servicos = _context.Servicos.ToList();
            var agendamento = _context.Agendamentos
                  .Where(a =>
                    a.TenantId == tenantId &&
                    a.BarbeiroId == barbeiroId &&
                    a.Status == AgendamentoStatus.Agendado
                ).ToList();
            // Agora processamos em memória (LINQ sobre objetos)
            return agendamento
                .Join(
                    servicos,
                    a => a.ServicoId.Value,
                    s => s.Id,
                    (a, s) => new
                    {
                        DataHoraAgendada = a.DataHoraAgendada,
                        Duracao = s.DuracaoMinutos
                    }
                )
                .Any(a =>
                    dataHoraInicio < a.DataHoraAgendada.AddMinutes(a.Duracao) &&
                    dataHoraFim > a.DataHoraAgendada
                );


            //return (from a in _context.Agendamentos
            //        join s in _context.Servicos on a.ServicoId.Value equals s.Id
            //        where a.TenantId == tenantId
            //              && a.BarbeiroId == barbeiroId
            //              && a.Status == AgendamentoStatus.Agendado
            //        select new
            //        {
            //            a.DataHoraAgendada,
            //            Duracao = s.DuracaoMinutos
            //        })
            //    .Any(a =>
            //        dataHoraInicio < a.DataHoraAgendada.AddMinutes(a.Duracao)
            //        && dataHoraFim > a.DataHoraAgendada);
        }

        public Agendamento? ObterPorId(Guid agendamentoId, TenantId tenantId)
        {
            return _context.Agendamentos.FirstOrDefault(a => a.Id == agendamentoId && a.TenantId == tenantId);

        }
        public IEnumerable<(DateTime DataHoraAgendada, int DuracaoMinutos)> ObterAgendamentosComServico(TenantId tenantId, BarbeiroId barbeiroId, DateTime dataReferencia)
		{
			return (from a in _context.Agendamentos
					join s in _context.Servicos on a.ServicoId.Value equals s.Id
					where a.TenantId == tenantId
						  && a.BarbeiroId == barbeiroId
						  && a.Status == AgendamentoStatus.Agendado
						  && a.DataHoraAgendada.Date == dataReferencia.Date
					select new ValueTuple<DateTime, int>(a.DataHoraAgendada, s.DuracaoMinutos))
				.ToList();

			var servicos = _context.Servicos.ToList();

            var resultado = _context.Agendamentos
                .Where(a =>
                    a.TenantId == tenantId &&
                    a.BarbeiroId == barbeiroId &&
                    a.Status == AgendamentoStatus.Agendado &&
                    a.DataHoraAgendada.Date == dataReferencia.Date
                )
                .AsEnumerable() // aqui força o LINQ rodar em memória (após vir do banco)
                .Select(a =>
                {
                    var servico = servicos.FirstOrDefault(s => s.Id == a.ServicoId.Value);
                    return (a.DataHoraAgendada, servico?.DuracaoMinutos ?? 0);
                })
                .ToList();

            return resultado;
        }
    }


}
