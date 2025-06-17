using AutoMapper;
using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Application.DTOs.HorarioDisponivel;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Inputs;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.Mappings
{
	public class HorarioDisponivelProfileMapper : Profile
    {
        public HorarioDisponivelProfileMapper()
        {
            //CreateMap<HorarioDisponivelRequest, HorarioDisponivel>()
            //    .ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));

            CreateMap<HorarioDisponivelRequest, HorarioDisponivel>()
            .ForMember(dest => dest.BarbeiroId, opt => opt.MapFrom(src => src.BarbeiroId))
            .ForMember(dest => dest.DiaSemana, opt => opt.MapFrom(src => src.DiaSemana))
            .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio))
            .ForMember(dest => dest.HoraFim, opt => opt.MapFrom(src => src.HoraFim))
            ;

            CreateMap<HorarioDisponivel, HorarioDisponivelResponse>()
               .ForMember(dest => dest.BarbeiroId, opt => opt.MapFrom(src => src.BarbeiroId))
               .ForMember(dest => dest.DiaSemana, opt => opt.MapFrom(src => src.DiaSemana))
               .ForMember(dest => dest.HoraInicio, opt => opt.MapFrom(src => src.HoraInicio))
               .ForMember(dest => dest.HoraFim, opt => opt.MapFrom(src => src.HoraFim))
               ;
        }
    }
}
