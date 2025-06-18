using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
	public interface IServicoRepository
	{
		void Adicionar(Servico servico);
		void Salvar();
		Servico? ObterPorId(TenantId tenantId, Guid servicoId);
		IEnumerable<Servico>? ListarServicosPorTenant(TenantId tenantId);
	}

}
