using Barbearia.Application.DTOs.Servico;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class ServicoService : IServicoService
    {
        private readonly BarbeariaDbContext _context;

        public ServicoService(BarbeariaDbContext context)
        {
            _context = context;
        }

        public ServicoResponse AdicionarServico(Guid tenantId, ServicoRequest request)
        {
            var servico = new Servico
            {
                Id = Guid.NewGuid(),
                TenantId = tenantId,
                Nome = request.Nome,
                Descricao = request.Descricao,
                DuracaoMinutos = request.DuracaoMinutos,
                Preco = request.Preco
            };

            _context.Servicos.Add(servico);
            _context.SaveChanges();

            return new ServicoResponse
            {
                Id = servico.Id,
                Nome = servico.Nome,
                Descricao = servico.Descricao,
                DuracaoMinutos = servico.DuracaoMinutos,
                Preco = servico.Preco
            };
        }

        public IEnumerable<ServicoResponse> ListarServicos(Guid tenantId)
        {
            return _context.Servicos
                .Where(s => s.TenantId == tenantId)
                .Select(s => new ServicoResponse
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Preco = s.Preco
                }).ToList();
        }
    }
}
