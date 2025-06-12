using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Interfaces
{
	public interface IAuthService
	{
		string Login(Guid tenantId, string email, string senha);
	}
}
