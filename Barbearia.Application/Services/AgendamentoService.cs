using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.DTOs.Status;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Exceptions;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class AgendamentoService : IAgendamentoService
    {
        private readonly BarbeariaDbContext _context;
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IServicoRepository _servicoRepository;

        public AgendamentoService(BarbeariaDbContext context, IAgendamentoRepository agendamentoRepository, IServicoRepository servicoRepository)
        {
            _context               = context;
            _agendamentoRepository = agendamentoRepository;
            _servicoRepository     = servicoRepository;
        }

        public void Agendar(Guid tenantId, AgendamentoRequest request)
        {
            var dataHoraNormalizada = request.DataHora.ToUniversalTime();

            if (dataHoraNormalizada < DateTime.UtcNow.AddMinutes(10))
                throw new BusinessException("O horário deve ser com pelo menos 10 minutos de antecedência.");

            var servico = _servicoRepository.ObterPorId(tenantId, request.ServicoId);
            if (servico == null)
                throw new BusinessException("Serviço não encontrado.");

            var dataHoraFim = dataHoraNormalizada.AddMinutes(servico.DuracaoMinutos);

            if (_agendamentoRepository.ClientePossuiDuplicado(tenantId, request.TelefoneCliente, dataHoraNormalizada))
                throw new BusinessException("Já existe um agendamento para este cliente neste horário.");

            if (_agendamentoRepository.ExisteConflito(tenantId, request.BarbeiroId, dataHoraNormalizada, dataHoraFim))
                throw new BusinessException("Já existe um agendamento conflitando com este horário.");

            var diaSemana = (int)dataHoraNormalizada.DayOfWeek;
            var horaInicio = dataHoraNormalizada.TimeOfDay;
            var horaFim = horaInicio.Add(TimeSpan.FromMinutes(servico.DuracaoMinutos));

            bool dentroDisponibilidade = _context.HorariosDisponiveis.Any(h =>
                h.BarbeiroId == request.BarbeiroId &&
                h.DiaSemana == diaSemana &&
                h.HoraInicio <= horaInicio &&
                h.HoraFim >= horaFim);

            if (!dentroDisponibilidade)
                throw new BusinessException("O horário solicitado não está dentro da jornada do barbeiro.");

            //Utilizar AutoMapper
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

            _agendamentoRepository.Adicionar(agendamento);
            _agendamentoRepository.Salvar();
        }

        public IEnumerable<Agendamento> ListarPorTenant(Guid tenantId)
        {
            return _agendamentoRepository.ListarPorTenant(tenantId);
        }

        public IEnumerable<DateTime> ListarDisponibilidade(Guid tenantId, Guid barbeiroId, Guid servicoId, DateTime dataReferencia)
        {
            var servico = _servicoRepository.ObterPorId(tenantId, servicoId);
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
