using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class ServicoRepository : BaseRepository<Servico>, IServicoRepository
    {
        public ServicoRepository(BarbeariaDbContext context) : base(context) { }

        public Servico? ObterPorId(Guid tenantId, Guid servicoId)
        {
            return _context.Servicos.FirstOrDefault(s => s.Id == servicoId && s.TenantId == tenantId);
        }
        public IEnumerable<Servico> ListarServicosPorTenant(Guid tenantId)
        {
            return _context.Servicos
           .Where(s => s.TenantId == tenantId)
           .ToList();
        }
    }
}
