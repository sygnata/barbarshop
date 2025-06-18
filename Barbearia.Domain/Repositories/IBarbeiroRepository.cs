using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
    public interface IBarbeiroRepository
    {
        void Adicionar(Barbeiro barbeiro);
        void Salvar();
        IEnumerable<Barbeiro> ListarBarbeiros(TenantId tenantId);
        bool ExisteComMesmoNome(TenantId tenantId, NomeBarbeiro nome);
    }
}
