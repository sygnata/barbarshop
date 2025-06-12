using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities
{
    public class Tenant
    {
        public Guid Id { get; set; }
        public string NomeFantasia { get; set; }
        public string LogoUrl { get; set; }
        public string CorPrimaria { get; set; }
        public string DominioCustomizado { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
