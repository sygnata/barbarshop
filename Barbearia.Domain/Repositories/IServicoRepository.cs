using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
	public interface IServicoRepository
	{
		Servico? ObterPorId(Guid tenantId, Guid servicoId);
	}

}
