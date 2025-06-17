using AutoMapper;
using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Domain.Inputs;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.Mappings
{
	public class BarbeiroProfileMapper : Profile
    {
        public BarbeiroProfileMapper()
        {
            CreateMap<BarbeiroRequest, BarbeiroInput>()
                //.ForCtorParam("nome", opt => opt.MapFrom(src => new NomeBarbeiro(src.Nome)))
                .ForCtorParam("nome", opt => opt.MapFrom(src => src.Nome));
        }
    }
}
