using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class BaseRepository<T> where T : class
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

        public void Adicionar(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Atualizar(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        public void Remover(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}
