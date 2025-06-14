using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class BarbeiroService : IBarbeiroService
    {
        private readonly BarbeariaDbContext _context;
        private readonly IBarbeiroRepository _barbeiroRepository;

        public BarbeiroService(BarbeariaDbContext context, IBarbeiroRepository barbeiroRepository)
        {
            _context = context;
            _barbeiroRepository = barbeiroRepository;
        }

        public BarbeiroResponse AdicionarBarbeiro(Guid tenantId, BarbeiroRequest request)
        {
            var barbeiro = new Barbeiro
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Nome = request.Nome,
            };
            _barbeiroRepository.Adicionar(barbeiro);
            _barbeiroRepository.Salvar();

            return new BarbeiroResponse
            {
                Id = barbeiro.Id,
                Nome = barbeiro.Nome,
            };
        }

        public IEnumerable<BarbeiroResponse> ListarBarbeiros(Guid tenantId)
        {
            var barbeiro = _barbeiroRepository.ListarBarbeiros(tenantId)
                .Select(b => new BarbeiroResponse
                {
                    Id = b.Id,
                    Nome = b.Nome
                }).ToList();

            return barbeiro;
        }
    }
}
