using Barbearia.Application.DTOs.HorarioDisponivel;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class HorarioDisponivelService : IHorarioDisponivelService
    {
        private readonly IHorarioDisponivelRepository _horarioDisponivelRepository;
        private readonly BarbeariaDbContext _dbContext;

        public HorarioDisponivelService(IHorarioDisponivelRepository horarioDisponivelRepository, BarbeariaDbContext dbContext)
        {
            _horarioDisponivelRepository = horarioDisponivelRepository;
            _dbContext = dbContext; 
        }

        public HorarioDisponivelResponse Adicionar(Guid tenantId, HorarioDisponivelRequest request)
        {
            var horario = new HorarioDisponivel
            {
                Id = Guid.NewGuid(),
                BarbeiroId = request.BarbeiroId,
                DiaSemana = request.DiaSemana,
                HoraInicio = request.HoraInicio,
                HoraFim = request.HoraFim
            };

            _horarioDisponivelRepository.Adicionar(horario);
            _horarioDisponivelRepository.Salvar();

            return new HorarioDisponivelResponse
            {
                Id = horario.Id,
                BarbeiroId = horario.BarbeiroId,
                DiaSemana = horario.DiaSemana,
                HoraInicio = horario.HoraInicio,
                HoraFim = horario.HoraFim
            };
        }

        public IEnumerable<HorarioDisponivelResponse> ListarPorBarbeiro(Guid tenantId, Guid barbeiroId)
        {
            var barbeiros =  _horarioDisponivelRepository.ListarPorBarbeiro(barbeiroId)
                .Select(h => new HorarioDisponivelResponse
                {
                    Id = h.Id,
                    BarbeiroId = h.BarbeiroId,
                    DiaSemana = h.DiaSemana,
                    HoraInicio = h.HoraInicio,
                    HoraFim = h.HoraFim
                }).ToList();
            return barbeiros;
        }
    }

}
