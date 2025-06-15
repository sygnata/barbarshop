using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class BarbeiroRepository : BaseRepository<Barbeiro>, IBarbeiroRepository
    {
        public BarbeiroRepository(BarbeariaDbContext context) : base(context) { }


        public IEnumerable<Barbeiro> ListarBarbeiros(TenantId tenantId)
        {
            return _context.Barbeiros.Where(s => s.TenantId == tenantId).ToList();
        }
      
    }
}
