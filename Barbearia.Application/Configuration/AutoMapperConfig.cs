using AutoMapper;
using Barbearia.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Barbearia.Application.Configuration
{
	public static class AutoMapperConfig
    {
        public static void AddApplicationMappings(this IServiceCollection services)
        {
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AgendamentoProfileMapper>();
                cfg.AddProfile<BarbeiroProfileMapper>();
                // aqui você pode adicionar outros profiles
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
