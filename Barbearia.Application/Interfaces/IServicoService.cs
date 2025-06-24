using Barbearia.Application.DTOs.Servico;

namespace Barbearia.Application.Interfaces
{
	public interface IServicoService
    {
        ServicoResponse AdicionarServico(Guid tenantId, ServicoRequest request);
        IEnumerable<ServicoResponse> ListarServicos(Guid tenantId);
        void AtualizarServico(Guid servicoId, Guid tenantId, ServicoRequest request);
        void Inativar(Guid servicoId, Guid tenantId);
        void Ativar(Guid servicoId, Guid tenantId);
    }
}
