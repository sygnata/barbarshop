using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface ITenantRepository
    {
        void Salvar();
        void Adicionar(Tenant tenant);
        Usuario? ObterPorId(Guid tenantId, string email);
    }
}
