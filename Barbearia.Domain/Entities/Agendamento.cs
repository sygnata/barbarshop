using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Domain.Entities
{
    public class Agendamento
    {
        public Guid Id { get; set; }
        public Guid TenantId { get; set; }
        public Guid BarbeiroId { get; set; }
        public Guid ServicoId { get; set; }
        public DateTime DataHoraAgendada { get; set; }
        public string ClienteNome { get; set; }
        public string ClienteTelefone { get; set; }
        public string Status { get; set; } // AGENDADO, CANCELADO, CONCLUIDO
    }
}
