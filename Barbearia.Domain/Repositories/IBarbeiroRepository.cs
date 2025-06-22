using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
    public interface IBarbeiroRepository
    {
        void Adicionar(Barbeiro barbeiro);
        void Salvar();
        void Atualizar(Barbeiro barbeiro);
        void Remover(Barbeiro barbeiro);
        IEnumerable<Barbeiro> ListarBarbeiros(TenantId tenantId);
        bool ExisteComMesmoNome(TenantId tenantId, NomeBarbeiro nome);
        Barbeiro ObterPorId(BarbeiroId barbeiroId, TenantId tenantId, bool ativo = true);
    }
}
