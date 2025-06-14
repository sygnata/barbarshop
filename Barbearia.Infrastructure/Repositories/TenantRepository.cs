using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class TenantRepository : BaseRepository<Tenant>, ITenantRepository
    {
        public TenantRepository(BarbeariaDbContext context) : base(context) { }


        public Usuario? ObterPorId(Guid tenantId, string email)
        {
            return _context.Usuarios.FirstOrDefault(s => s.Email == email && s.TenantId == tenantId);
        }
      
    }
}
