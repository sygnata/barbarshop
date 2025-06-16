using Barbearia.Application.Interfaces;
using Barbearia.Application.Services;
using Barbearia.Domain.Factories;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Providers;
using Barbearia.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
	public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
			#region SERVICES
			services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IServicoService, ServicoService>();
            services.AddScoped<IBarbeiroService, BarbeiroService>();
            services.AddScoped<IAgendamentoService, AgendamentoService>();
            services.AddScoped<IHorarioDisponivelService, HorarioDisponivelService>();
			#endregion

			#region REPOSITORIES
			services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
			services.AddScoped<IServicoRepository, ServicoRepository>();
			services.AddScoped<IHorarioDisponivelRepository, HorarioDisponivelRepository>();
			services.AddScoped<IUsuarioRepository, UsuarioRepository>();
			services.AddScoped<IBarbeiroRepository, BarbeiroRepository>();
			services.AddScoped<ITenantRepository, TenantRepository>();
			services.AddScoped<IUnitOfWork, UnitOfWork>();
			#endregion

			#region FACTORIES
			services.AddScoped<TenantFactory>();
			#endregion
			#region PROVIDERS
			services.AddScoped<ITenantProvider, TenantProvider>();
			#endregion
			return services;
        }
    }
}
