using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IBarbeiroRepository
    {
        void Adicionar(Barbeiro barbeiro);
        void Salvar();
        IEnumerable<Barbeiro> ListarBarbeiros(Guid tenantId);
    }
}
