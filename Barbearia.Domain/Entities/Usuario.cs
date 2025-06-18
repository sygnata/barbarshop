using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Entities
{
	public class Usuario
    {
        public Guid Id { get; set; }
        public TenantId TenantId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string SenhaHash { get; set; }
        public string Perfil { get; set; } // ADMIN ou FUNCIONARIO
        public bool Ativo { get; private set; } = true;

        public void Inativar()
        {
            Ativo = false;
        }
    }

}
