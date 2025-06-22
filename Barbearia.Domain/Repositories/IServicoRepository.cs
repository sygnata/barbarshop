using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
	public interface IServicoRepository
	{
		void Adicionar(Servico servico);
		void Salvar();
		void Atualizar(Servico servico);
		void Remover(Servico servico);
		Servico? ObterPorId(TenantId tenantId, Guid servicoId, bool ativo = true);
		IEnumerable<Servico>? ListarServicosPorTenant(TenantId tenantId);
	}

}
