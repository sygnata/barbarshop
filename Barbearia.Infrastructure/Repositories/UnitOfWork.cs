using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BarbeariaDbContext _context;

        public UnitOfWork(BarbeariaDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
