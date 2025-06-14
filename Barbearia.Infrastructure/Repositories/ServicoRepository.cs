using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class ServicoRepository : IServicoRepository
    {
        private readonly BarbeariaDbContext _context;

        public ServicoRepository(BarbeariaDbContext context)
        {
            _context = context;
        }

        public Servico? ObterPorId(Guid tenantId, Guid servicoId)
        {
            return _context.Servicos.FirstOrDefault(s => s.Id == servicoId && s.TenantId == tenantId);
        }
    }
}
