using AutoMapper;
using Barbearia.Application.DTOs.HorarioDisponivel;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Exceptions;

namespace Barbearia.Application.Services
{
	public class HorarioDisponivelService : IHorarioDisponivelService
    {
        private readonly IHorarioDisponivelRepository _horarioDisponivelRepository;
        private readonly IMapper _mapper;


        public HorarioDisponivelService(IHorarioDisponivelRepository horarioDisponivelRepository, IMapper mapper)
        {
            _horarioDisponivelRepository = horarioDisponivelRepository;
            _mapper = mapper;
        }

        public HorarioDisponivelResponse Adicionar(Guid tenantId, HorarioDisponivelRequest request)
        {
            if (_horarioDisponivelRepository.ExisteConflitoHorario(request.BarbeiroId, request.DiaSemana, request.HoraInicio, request.HoraFim))
                throw new BusinessException("Já existe um horário cadastrado que conflita com este horário.");

            var input = _mapper.Map<HorarioDisponivel>(request);

            _horarioDisponivelRepository.Adicionar(input);
            _horarioDisponivelRepository.Salvar();

            var retorno = _mapper.Map<HorarioDisponivelResponse>(input);
            return retorno;
        
        }

        public IEnumerable<HorarioDisponivelResponse> ListarPorBarbeiro(Guid tenantId, Guid barbeiroId)
        {
            var barbeiros =  _horarioDisponivelRepository.ListarPorBarbeiro(barbeiroId, tenantId)
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
