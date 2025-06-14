using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class TenantService : ITenantService
    {
        private readonly BarbeariaDbContext _context;
        private readonly ITenantRepository _tenantRepository;
        private readonly IUsuarioRepository _usuarioRepository;

		public TenantService(BarbeariaDbContext context, ITenantRepository tenantRepository, IUsuarioRepository usuarioRepository)
		{
			_context = context;
            _tenantRepository = tenantRepository;
            _usuarioRepository = usuarioRepository;
		}

		public CreateTenantResponse CriarTenant(CreateTenantRequest request)
        {
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                NomeFantasia = request.NomeFantasia,
                DataCriacao = DateTime.UtcNow
            };

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                TenantId = tenant.Id,
                Nome = request.NomeAdmin,
                Email = request.EmailAdmin,
                SenhaHash = request.SenhaAdmin, //TODO Lembrando: aplicar hash real em produção
                Perfil = "ADMIN"
            };

            _tenantRepository.Adicionar(tenant);
            _usuarioRepository.Adicionar(usuario);
    
            _tenantRepository.Salvar();

            return new CreateTenantResponse
            {
                TenantId = tenant.Id,
                UsuarioAdminId = usuario.Id
            };
        }
    }
}
