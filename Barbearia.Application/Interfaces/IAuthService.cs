using Barbearia.Domain.ValueObjects;

namespace Barbearia.Application.Interfaces
{
	public interface IAuthService
	{
		string Login(TenantId tenantId, string email, string senha);
	}
}
