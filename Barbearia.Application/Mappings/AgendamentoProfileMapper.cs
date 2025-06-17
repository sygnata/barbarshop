using AutoMapper;
using Barbearia.Application.DTOs.Agendamento;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Inputs;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.Mappings
{
	public class AgendamentoProfileMapper : Profile
    {
        public AgendamentoProfileMapper()
        {
            CreateMap<AgendamentoRequest, AgendamentoInput>()
                .ForCtorParam("servicoId", opt => opt.MapFrom(src => new ServicoId(src.ServicoId)))
                .ForCtorParam("barbeiroId", opt => opt.MapFrom(src => new BarbeiroId(src.BarbeiroId)))
                .ForCtorParam("dataHoraAgendada", opt => opt.MapFrom(src => src.DataHora.ToUniversalTime()))
                .ForCtorParam("nomeCliente", opt => opt.MapFrom(src => new string(src.NomeCliente)))
                .ForCtorParam("telefoneCliente", opt => opt.MapFrom(src => new Telefone(src.TelefoneCliente)))
                .ForCtorParam("status", opt => opt.MapFrom(_ => AgendamentoStatus.Agendado));
        }
    }
}
