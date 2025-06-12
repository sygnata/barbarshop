using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Perfil { get; set; } // ADMIN ou FUNCIONARIO
    }

}
