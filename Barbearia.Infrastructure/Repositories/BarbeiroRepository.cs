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
            return _context.Barbeiros.Where(s => s.TenantId == tenantId && s.Ativo).ToList();
        }

        public bool ExisteComMesmoNome(TenantId tenantId, NomeBarbeiro nome)
        {
            return _context.Barbeiros
                .Any(b => b.TenantId == tenantId && b.Nome == nome);
        }

        public Barbeiro ObterPorId(BarbeiroId barbeiroId, TenantId tenantId, bool ativo = true)
        { 
            return _context.Barbeiros.Where(wh => wh.Id == barbeiroId && wh.TenantId == tenantId && wh.Ativo == ativo).FirstOrDefault();
        }
    }
}
