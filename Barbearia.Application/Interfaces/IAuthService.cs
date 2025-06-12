using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Interfaces
{
	public interface IAuthService
	{
		string Login(string email, string senha);
	}
}
