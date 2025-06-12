using Barbearia.Application.DTOs.Barbeiro;

namespace Barbearia.Application.Interfaces
{
	public interface IBarbeiroService
    {
        BarbeiroResponse AdicionarBarbeiro(Guid tenantId, BarbeiroRequest request);
        IEnumerable<BarbeiroResponse> ListarBarbeiros(Guid tenantId);
    }
}
