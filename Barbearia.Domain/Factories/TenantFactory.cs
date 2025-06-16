using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;
using System;

namespace Barbearia.Domain.Factories
{
    public class TenantFactory
    {
        public (Tenant tenant, Usuario usuarioAdmin) CriarComAdmin(
            NomeFantasia nomeFantasia, string nomeAdmin, string emailAdmin, string senhaAdmin)
        {
            var tenant = new Tenant
            {
                Id = Guid.NewGuid(),
                NomeFantasia = nomeFantasia,
                DataCriacao = DateTime.UtcNow
            };

            var usuario = new Usuario
            {
                Id = Guid.NewGuid(),
                TenantId = tenant.Id,
                Nome = nomeAdmin,
                Email = emailAdmin,
                SenhaHash = senhaAdmin,
                Perfil = "ADMIN"
            };

            return (tenant, usuario);
        }
    }
}
