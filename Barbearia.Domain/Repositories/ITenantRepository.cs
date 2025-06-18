using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
    public interface ITenantRepository
    {
        void Salvar();
        void Adicionar(Tenant tenant);
        Usuario? ObterPorId(TenantId tenantId, string email);
    }
}
