using Barbearia.Domain.ValueObjects;
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
        public TenantId TenantId { get; set; }//VALUEOBJECT
        public NomeServico Nome { get; set; }//VALUEOBJECT
        public DescricaoServico Descricao { get; set; }//VALUEOBJECT
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }//VALUEOBJECT
        public bool Ativo { get; private set; } = true;

        public void Inativar()
        {
            Ativo = false;
        }

        public void Ativar()
        {
            Ativo = true;
        }
    }

}
