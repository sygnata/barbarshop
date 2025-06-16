using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Factories;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class TenantService : ITenantService
    {
        private readonly ITenantRepository _tenantRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly TenantFactory _tenantFactory;

        public TenantService(ITenantRepository tenantRepository, IUsuarioRepository usuarioRepository, IUnitOfWork unitOfWork, TenantFactory tenantFactory)
		{
            _tenantRepository = tenantRepository;
            _usuarioRepository = usuarioRepository;
            _unitOfWork = unitOfWork;
            _tenantFactory = tenantFactory;
        }

		public CreateTenantResponse CriarTenant(CreateTenantRequest request)
        {
            var (tenant, usuario) = _tenantFactory.CriarComAdmin(
                request.NomeFantasia,
                request.NomeAdmin,
                request.EmailAdmin,
                request.SenhaAdmin
                );
     
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
