using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.Interfaces
{
    public interface ITenantService
    {
        CreateTenantResponse CriarTenant(CreateTenantRequest request);
    }
}
