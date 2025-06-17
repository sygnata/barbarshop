using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Inputs
{
    public class BarbeiroInput
    {
        public TenantId TenantId { get; private set; }
        public string Nome { get; private set; }

        public void SetTenantId(TenantId tenantId)
        {
            TenantId = tenantId;
        }

        public BarbeiroInput(           
            string nome
           )
        {
            Nome = nome;
        }
    }
}
