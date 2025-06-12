using Barbearia.Application.DTOs.Barbeiro;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Services
{
    public class BarbeiroService : IBarbeiroService
    {
        private readonly BarbeariaDbContext _context;

        public BarbeiroService(BarbeariaDbContext context)
        {
            _context = context;
        }

        public BarbeiroResponse AdicionarBarbeiro(Guid tenantId, BarbeiroRequest request)
        {
            var barbeiro = new Barbeiro
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Nome = request.Nome,
            };

            _context.Barbeiros.Add(barbeiro);
            _context.SaveChanges();

            return new BarbeiroResponse
            {
                Id = barbeiro.Id,
                Nome = barbeiro.Nome,
            };
        }

        public IEnumerable<BarbeiroResponse> ListarBarbeiros(Guid tenantId)
        {
            return _context.Barbeiros
                .Where(b => b.TenantId == tenantId)
                .Select(b => new BarbeiroResponse
                {
                    Id = b.Id,
                    Nome = b.Nome
                }).ToList();
        }
    }
}
