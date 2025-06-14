using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
	public interface IServicoRepository
	{
		void Adicionar(Servico servico);
		void Salvar();
		Servico? ObterPorId(Guid tenantId, Guid servicoId);
		IEnumerable<Servico>? ListarServicosPorTenant(Guid tenantId);
	}

}
