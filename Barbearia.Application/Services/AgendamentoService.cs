using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
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
            // 1️⃣ Validação: impedir conflito de horário para o barbeiro
            bool existeConflito = _context.Agendamentos.Any(a =>
                a.TenantId == tenantId &&
                a.BarbeiroId == request.BarbeiroId &&
                a.DataHoraAgendada == request.DataHora);

            if (existeConflito)
            {
                throw new BusinessException("Já existe um agendamento para este barbeiro neste horário.");
            }

            // 2️⃣ Validação: verificar se o horário solicitado está dentro do horário disponível configurado
            var diaSemana = (int)request.DataHora.DayOfWeek;
            var hora = request.DataHora.TimeOfDay;

            bool dentroDisponibilidade = _context.HorariosDisponiveis.Any(h =>
                h.BarbeiroId == request.BarbeiroId &&
                h.DiaSemana == diaSemana &&
                h.HoraInicio <= hora &&
                h.HoraFim >= hora);

            if (!dentroDisponibilidade)
            {
                throw new BusinessException("O horário solicitado não está dentro da disponibilidade configurada para o barbeiro.");
            }

            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                ServicoId = request.ServicoId,
                BarbeiroId = request.BarbeiroId,
                DataHoraAgendada = request.DataHora,
                ClienteNome = request.NomeCliente,
                ClienteTelefone = request.TelefoneCliente,
                Status = "Agendado"
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
