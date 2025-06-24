using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(BarbeariaDbContext context) : base(context) { }


        public Usuario? ObterPorId(TenantId tenantId, string email)
        {
            return _context.Usuarios.FirstOrDefault(s => s.Email == email && s.TenantId == tenantId && s.Ativo);
        }
      
    }
}
