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
        public Guid TenantId { get; set; }//VALUEOBJECT
        public string Nome { get; set; }//VALUEOBJECT
        public string Descricao { get; set; }//VALUEOBJECT
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }//VALUEOBJECT
    }

}
