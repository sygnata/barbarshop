using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.DTOs
{
    public class CreateTenantResponse
    {
        public Guid TenantId { get; set; }
        public Guid UsuarioAdminId { get; set; }
    }
}
