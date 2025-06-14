using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario? ObterPorId(Guid tenantId, string email);
    }
}
