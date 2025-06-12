using Barbearia.Application.DTOs.Servico;

namespace Barbearia.Application.Interfaces
{
	public interface IServicoService
    {
        ServicoResponse AdicionarServico(Guid tenantId, ServicoRequest request);
        IEnumerable<ServicoResponse> ListarServicos(Guid tenantId);
    }
}
