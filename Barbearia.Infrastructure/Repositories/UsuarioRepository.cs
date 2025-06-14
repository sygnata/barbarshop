using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Barbearia.Infrastructure.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(BarbeariaDbContext context) : base(context) { }


        public Usuario? ObterPorId(Guid tenantId, string email)
        {
            return _context.Usuarios.FirstOrDefault(s => s.Email == email && s.TenantId == tenantId);
        }
      
    }
}
