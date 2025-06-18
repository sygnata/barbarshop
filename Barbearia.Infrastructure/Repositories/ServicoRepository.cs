using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class ServicoRepository : BaseRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(BarbeariaDbContext context) : base(context) { }

        public Servico? ObterPorId(TenantId tenantId, Guid servicoId)
        {
            return _context.Servicos.FirstOrDefault(s => s.Id == servicoId && s.TenantId == tenantId);
        }
        public IEnumerable<Servico> ListarServicosPorTenant(TenantId tenantId)
        {
            return _context.Servicos
           .Where(s => s.TenantId == tenantId)
           .ToList();
        }
    }
}
