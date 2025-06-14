using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;

		public TenantService(ITenantRepository tenantRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork)
		{
            _tenantRepository = tenantRepository;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
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
            _unitOfWork.Commit();

            return new CreateTenantResponse
            {
                TenantId = tenant.Id,
                UsuarioAdminId = usuario.Id
            };
        }
    }
}
