using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        void Salvar();
        void Adicionar(Usuario usuario);
        Usuario? ObterPorId(Guid tenantId, string email);
    }
}
