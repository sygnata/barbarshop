using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities
{
    public class Barbeiro
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Nome { get; set; }
        public string FotoUrl { get; set; }
        public string Descricao { get; set; }
    }
}
