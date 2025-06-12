using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities
{
    public class Servico
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }
    }

}
