using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public abstract class BaseRepository
    {
        protected readonly BarbeariaDbContext _context;

        protected BaseRepository(BarbeariaDbContext context)
        {
            _context = context;
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }
    }
}
