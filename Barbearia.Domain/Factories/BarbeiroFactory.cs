using Barbearia.Domain.Entities;
using Barbearia.Domain.Entities.Enums;
using Barbearia.Domain.Inputs;

namespace Barbearia.Domain.Factories
{
    public class BarbeiroFactory
    {
        public Barbeiro CriarBarbeiro(BarbeiroInput? input)
        {
            return new Barbeiro
            {
                Id = Guid.NewGuid(),
                TenantId = input.TenantId,
                Nome = input.Nome,
            };
        }
    }
}
