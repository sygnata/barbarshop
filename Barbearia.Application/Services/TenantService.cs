using Barbearia.Application.DTOs;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Services
{
    public class TenantService : ITenantService
    {
        private readonly BarbeariaDbContext _context;

        public TenantService(BarbeariaDbContext context)
        {
            _context = context;
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

            _context.Tenants.Add(tenant);
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return new CreateTenantResponse
            {
                TenantId = tenant.Id,
                UsuarioAdminId = usuario.Id
            };
        }
    }
}
