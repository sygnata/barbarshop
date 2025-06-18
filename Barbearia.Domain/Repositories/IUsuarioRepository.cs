using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        void Salvar();
        void Adicionar(Usuario usuario);
        Usuario? ObterPorId(TenantId tenantId, string email);
    }
}
