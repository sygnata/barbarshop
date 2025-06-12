using Barbearia.Application.Interfaces;
using Barbearia.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
	public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IBarbeiroService, BarbeiroService>();

            return services;
        }
    }
}
