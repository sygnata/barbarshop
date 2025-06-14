using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.DTOs.Status;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Infrastructure.Exceptions;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class AgendamentoService : IAgendamentoService
    {
        private readonly BarbeariaDbContext _context;

        public AgendamentoService(BarbeariaDbContext context)
        {
            _context = context;
        }

        public AgendamentoResponse Agendar(Guid tenantId, AgendamentoRequest request)
        {
            var dataHoraNormalizada = request.DataHora.ToUniversalTime();

            // Regra 1 - Não permitir agendamento no passado ou muito em cima da hora
            if (dataHoraNormalizada < DateTime.UtcNow.AddMinutes(10))
                throw new BusinessException("O horário deve ser com pelo menos 10 minutos de antecedência.");

            // Buscar a duração do serviço informado
            var servico = _context.Servicos.FirstOrDefault(s => s.Id == request.ServicoId && s.TenantId == tenantId);
            if (servico == null)
                throw new BusinessException("Serviço informado não encontrado.");

            var dataHoraFim = dataHoraNormalizada.AddMinutes(servico.DuracaoMinutos);

            // Regra 2 - Cliente não pode ter dois agendamentos no mesmo horário
            bool clienteDuplicado = _context.Agendamentos.Any(a =>
                a.TenantId == tenantId &&
                a.ClienteTelefone == request.TelefoneCliente &&
                a.DataHoraAgendada == dataHoraNormalizada &&
                a.Status == AgendamentoStatus.Agendado);

            if (clienteDuplicado)
                throw new BusinessException("Já existe um agendamento para este cliente neste horário.");

            // Carrega todos os agendamentos ativos com duracao para este barbeiro
            var agendamentosExistentes = (from a in _context.Agendamentos
                                          join s in _context.Servicos on a.ServicoId equals s.Id
                                          where a.TenantId == tenantId
                                              && a.BarbeiroId == request.BarbeiroId
                                              && a.Status == AgendamentoStatus.Agendado
                                          select new
                                          {
                                              a.DataHoraAgendada,
                                              Duracao = s.DuracaoMinutos
                                          }).ToList();

            bool existeConflito = agendamentosExistentes.Any(a =>
                dataHoraNormalizada < a.DataHoraAgendada.AddMinutes(a.Duracao) &&
                dataHoraFim > a.DataHoraAgendada);

            if (existeConflito)
            {
                throw new BusinessException("Já existe um agendamento conflitando com este horário.");
            }

            var diaSemana = (int)request.DataHora.DayOfWeek;
            var horaInicio = request.DataHora.TimeOfDay;
            var horaFim = horaInicio.Add(TimeSpan.FromMinutes(servico.DuracaoMinutos));

            // Validação - Não ultrapassar disponibilidade do barbeiro
            bool dentroDisponibilidade = _context.HorariosDisponiveis.Any(h =>
                h.BarbeiroId == request.BarbeiroId &&
                h.DiaSemana == diaSemana &&
                h.HoraInicio <= horaInicio &&
                h.HoraFim >= horaFim);

            if (!dentroDisponibilidade)
            {
                throw new BusinessException("O horário solicitado não está disponível dentro da jornada do barbeiro.");
            }

            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                ServicoId = request.ServicoId,
                BarbeiroId = request.BarbeiroId,
                DataHoraAgendada = dataHoraNormalizada,
                ClienteNome = request.NomeCliente,
                ClienteTelefone = request.TelefoneCliente,
                Status = AgendamentoStatus.Agendado
            };

            _context.Agendamentos.Add(agendamento);
            _context.SaveChanges();

            return new AgendamentoResponse
            {
                Id = agendamento.Id,
                ServicoId = agendamento.ServicoId,
                BarbeiroId = agendamento.BarbeiroId,
                DataHora = agendamento.DataHoraAgendada,
                NomeCliente = agendamento.ClienteNome,
                TelefoneCliente = agendamento.ClienteTelefone
            };
        }

        public IEnumerable<DateTime> ListarDisponibilidade(Guid tenantId, Guid barbeiroId, Guid servicoId, DateTime dataReferencia)
        {
            var servico = _context.Servicos.FirstOrDefault(s => s.Id == servicoId && s.TenantId == tenantId);
            if (servico == null)
                throw new BusinessException("Serviço não encontrado.");

            var diaSemana = (int)dataReferencia.DayOfWeek;
            var disponibilidade = _context.HorariosDisponiveis.FirstOrDefault(h =>
                h.BarbeiroId == barbeiroId &&
                h.DiaSemana == diaSemana);

            if (disponibilidade == null)
                return Enumerable.Empty<DateTime>();

            var agendamentos = (from a in _context.Agendamentos
                                join s in _context.Servicos on a.ServicoId equals s.Id
                                where a.TenantId == tenantId
                                    && a.BarbeiroId == barbeiroId
                                    && a.Status == AgendamentoStatus.Agendado
                                    && a.DataHoraAgendada.Date == dataReferencia.Date
                                select new
                                {
                                    a.DataHoraAgendada,
                                    Duracao = s.DuracaoMinutos
                                }).ToList();

            var horariosDisponiveis = new List<DateTime>();
            var inicio = dataReferencia.Date.Add(disponibilidade.HoraInicio);
            var fim = dataReferencia.Date.Add(disponibilidade.HoraFim);

            for (var hora = inicio; hora.AddMinutes(servico.DuracaoMinutos) <= fim; hora = hora.AddMinutes(20))
            {
                var horaFim = hora.AddMinutes(servico.DuracaoMinutos);
                var conflito = agendamentos.Any(a =>
                    hora < a.DataHoraAgendada.AddMinutes(a.Duracao) &&
                    horaFim > a.DataHoraAgendada);

                if (!conflito)
                {
                    horariosDisponiveis.Add(hora);
                }
            }

            return horariosDisponiveis;
        }

        public void AlterarStatus(Guid tenantId, AlterarStatusRequest request)
        {
            var agendamento = _context.Agendamentos.FirstOrDefault(a => a.Id == request.AgendamentoId && a.TenantId == tenantId);

            if (agendamento == null)
                throw new BusinessException("Agendamento não encontrado.");

            agendamento.Status = (AgendamentoStatus)request.NovoStatus;
            _context.SaveChanges();
        }

        public IEnumerable<AgendamentoResponse> ListarAgendamentos(Guid tenantId)
        {
            return _context.Agendamentos
                .Where(a => a.TenantId == tenantId)
                .Select(a => new AgendamentoResponse
                {
                    Id = a.Id,
                    ServicoId = a.ServicoId,
                    BarbeiroId = a.BarbeiroId,
                    DataHora = a.DataHoraAgendada,
                    NomeCliente = a.ClienteNome,
                    TelefoneCliente = a.ClienteTelefone
                }).ToList();
        }
    }

}
