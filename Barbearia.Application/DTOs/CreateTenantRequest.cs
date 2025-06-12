using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Application.DTOs
{
    public class CreateTenantRequest
    {
        public string NomeFantasia { get; set; }
        public string EmailAdmin { get; set; }
        public string SenhaAdmin { get; set; }
        public string NomeAdmin { get; set; }
    }
}
