using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Application.DTOs.Status;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Exceptions;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class AgendamentoService : IAgendamentoService
    {
        private readonly IAgendamentoRepository _agendamentoRepository;
        private readonly IServicoRepository _servicoRepository;
        private readonly IHorarioDisponivelRepository _horarioDisponivelRepository;

        public AgendamentoService(IAgendamentoRepository agendamentoRepository, IServicoRepository servicoRepository, IHorarioDisponivelRepository horarioDisponivelRepository)
        {
            _agendamentoRepository       = agendamentoRepository;
            _servicoRepository           = servicoRepository;
            _horarioDisponivelRepository = horarioDisponivelRepository;
        }

        public void Agendar(Guid tenantId, AgendamentoRequest request)
        {
            var dataHoraNormalizada = request.DataHora.ToUniversalTime();

            if (dataHoraNormalizada < DateTime.UtcNow.AddMinutes(10))
                throw new BusinessException("O horário deve ser com pelo menos 10 minutos de antecedência.");

            var tenant = new TenantId(tenantId);
            var servico = _servicoRepository.ObterPorId(tenant, request.ServicoId);
            if (servico == null)
                throw new BusinessException("Serviço não encontrado.");

            var dataHoraFim = dataHoraNormalizada.AddMinutes(servico.DuracaoMinutos);

            if (_agendamentoRepository.ClientePossuiDuplicado(tenantId, request.TelefoneCliente, dataHoraNormalizada))
                throw new BusinessException("Já existe um agendamento para este cliente neste horário.");

            if (_agendamentoRepository.ExisteConflito(tenantId, request.BarbeiroId, dataHoraNormalizada, dataHoraFim))
                throw new BusinessException("Já existe um agendamento conflitando com este horário.");

            var diaSemana = (int)dataHoraNormalizada.DayOfWeek;
           
            var disponibilidade = _horarioDisponivelRepository.ObterPorBarbeiroDia(request.BarbeiroId, diaSemana);
            
            if (disponibilidade == null)
                throw new BusinessException("O barbeiro não possui disponibilidade cadastrada para este dia.");

            var horaInicio = dataHoraNormalizada.TimeOfDay;
            var horaFim = horaInicio.Add(TimeSpan.FromMinutes(servico.DuracaoMinutos));
            if (horaInicio < disponibilidade.HoraInicio || horaFim > disponibilidade.HoraFim)
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

        public IEnumerable<DateTime> ListarDisponibilidade(Guid tenantId, Guid barbeiroId, Guid servicoId, DateTime dataReferencia)
        {
            var servico = _servicoRepository.ObterPorId(tenantId, servicoId);
            if (servico == null)
                throw new BusinessException("Serviço não encontrado.");

            var diaSemana = (int)dataReferencia.DayOfWeek;
            var disponibilidade = _horarioDisponivelRepository.ObterPorBarbeiroDia(barbeiroId, diaSemana);

            if (disponibilidade == null)
                return Enumerable.Empty<DateTime>();

            var agendamentos = _agendamentoRepository.ObterAgendamentosComServico(tenantId, barbeiroId, dataReferencia);

            var horariosDisponiveis = new List<DateTime>();
            var inicio = dataReferencia.Date.Add(disponibilidade.HoraInicio);
            var fim = dataReferencia.Date.Add(disponibilidade.HoraFim);
            for (var hora = inicio; hora.AddMinutes(servico.DuracaoMinutos) <= fim; hora = hora.AddMinutes(20))
            {
                var horaFim = hora.AddMinutes(servico.DuracaoMinutos);
                var conflito = agendamentos.Any(a =>
                    hora < a.DataHoraAgendada.AddMinutes(a.DuracaoMinutos) &&
                    horaFim > a.DataHoraAgendada);

                if (!conflito)
                    horariosDisponiveis.Add(hora);
            }

            return horariosDisponiveis;
        }

        public void AlterarStatus(Guid tenantId, AlterarStatusRequest request)
        {
            var agendamento = _agendamentoRepository.ObterPorId(request.AgendamentoId, tenantId);
            if (agendamento == null)
                throw new BusinessException("Agendamento não encontrado.");

            agendamento.Status = (AgendamentoStatus)request.NovoStatus;
            _agendamentoRepository.Salvar();
        }

        public IEnumerable<AgendamentoResponse> ListarAgendamentos(Guid tenantId)
        {
            var agendamentos = _agendamentoRepository.ListarPorTenant(tenantId);
            return agendamentos.Select(a => new AgendamentoResponse
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
