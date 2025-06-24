using Barbearia.Application.DTOs.Barbeiro;

namespace Barbearia.Application.Interfaces
{
	public interface IBarbeiroService
    {
        BarbeiroResponse AdicionarBarbeiro(Guid tenantId, BarbeiroRequest request);
        IEnumerable<BarbeiroResponse> ListarBarbeiros(Guid tenantId);
        void AtualizarBarbeiro(Guid tenantId, Guid barbeiroId, BarbeiroRequest request);
        void Inativar(Guid barbeiroId, Guid tenantId);
        void Ativar(Guid barbeiroId, Guid tenantId);
    }
}
