using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var agendamento = new Agendamento
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                ServicoId = request.ServicoId,
                BarbeiroId = request.BarbeiroId,
                DataHoraAgendada = request.DataHora,
                ClienteNome = request.NomeCliente,
                ClienteTelefone = request.TelefoneCliente
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
