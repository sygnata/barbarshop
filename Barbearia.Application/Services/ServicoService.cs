using Barbearia.Application.DTOs.Servico;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Application.Services
{
	public class ServicoService : IServicoService
    {
        private readonly IServicoRepository _servicoRepository;


        public ServicoService(IServicoRepository servicoRepository)
        {
            _servicoRepository = servicoRepository;
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

            _servicoRepository.Adicionar(servico);
            _servicoRepository.Salvar();

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
            var tenant = new TenantId(tenantId);
            var servicos =  _servicoRepository.ListarServicosPorTenant(tenant)
                .Select(s => new ServicoResponse
                {
                    Id = s.Id,
                    Nome = s.Nome,
                    Descricao = s.Descricao,
                    DuracaoMinutos = s.DuracaoMinutos,
                    Preco = s.Preco
                })
                .ToList();
            return servicos;
        }
    }
}
