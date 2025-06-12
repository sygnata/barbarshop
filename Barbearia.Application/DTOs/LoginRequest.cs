using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.DTOs
{
    public class LoginRequest
    {
        public Guid TenantId { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }
}
