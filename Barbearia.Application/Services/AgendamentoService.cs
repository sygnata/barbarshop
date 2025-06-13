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

            // Buscar a duração do serviço informado
            var servico = _context.Servicos.FirstOrDefault(s => s.Id == request.ServicoId && s.TenantId == tenantId);
            if (servico == null)
                throw new BusinessException("Serviço informado não encontrado.");

            var dataHoraFim = dataHoraNormalizada.AddMinutes(servico.DuracaoMinutos);

            // Validação 1 - Conflito de agendamento considerando duração
            bool existeConflito = _context.Agendamentos.Any(a =>
                a.TenantId == tenantId &&
                a.BarbeiroId == request.BarbeiroId &&
                a.Status == AgendamentoStatus.Agendado &&
                (dataHoraNormalizada < a.DataHoraAgendada.AddMinutes(
                    _context.Servicos.Where(s => s.Id == a.ServicoId).Select(s => s.DuracaoMinutos).FirstOrDefault()) &&
                 dataHoraFim > a.DataHoraAgendada)
            );

            if (existeConflito)
            {
                throw new BusinessException("Já existe um agendamento conflitando com este horário.");
            }

            var diaSemana = (int)request.DataHora.DayOfWeek;
            var horaInicio = request.DataHora.TimeOfDay;
            var horaFim = horaInicio.Add(TimeSpan.FromMinutes(servico.DuracaoMinutos));

            // Validação 2 - Não ultrapassar disponibilidade do barbeiro
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
