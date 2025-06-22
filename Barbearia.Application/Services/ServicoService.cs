using Barbearia.Application.DTOs.Servico;
using Barbearia.Application.Interfaces;
using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Exceptions;

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
            //var tenant = new TenantId(tenantId);
            var servicos =  _servicoRepository.ListarServicosPorTenant(tenantId)
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

        public void AtualizarServico(Guid servicoId, Guid tenantId, ServicoRequest request)
        {
            var servico = _servicoRepository.ObterPorId(tenantId, servicoId);

            if (servico == null)
                throw new BusinessException("Serviço não encontrado ou inativo.");

            servico.Nome = request.Nome;
            servico.Descricao = request.Descricao;
            servico.DuracaoMinutos = request.DuracaoMinutos;
            servico.Preco = request.Preco;

            _servicoRepository.Atualizar(servico);
            _servicoRepository.Salvar();
        }

        public void Inativar(Guid servicoId, Guid tenantId)
        {
            var servico = _servicoRepository.ObterPorId(tenantId, servicoId);
            if (servico == null)
                throw new BusinessException("Serviço não encontrado ou inativo.");

            servico.Inativar();
            _servicoRepository.Salvar();
        }

        public void Ativar(Guid servicoId, Guid tenantId)
        {
            var servico = _servicoRepository.ObterPorId(tenantId, servicoId, false);
            if (servico == null)
                throw new BusinessException("Serviço não encontrado ou ativo.");

            servico.Ativar();
            _servicoRepository.Salvar();
        }
    }
}
