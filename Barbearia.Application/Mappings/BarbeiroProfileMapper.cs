using AutoMapper;
using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Inputs;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.Mappings
{
	public class BarbeiroProfileMapper : Profile
    {
        public BarbeiroProfileMapper()
        {
            CreateMap<BarbeiroRequest, BarbeiroInput>()
                .ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));

            CreateMap<Barbeiro, BarbeiroResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome.Value));
        }
    }
}
