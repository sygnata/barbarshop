using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class HorarioDisponivelRepository : IHorarioDisponivelRepository
    {
        private readonly BarbeariaDbContext _context;

        public HorarioDisponivelRepository(BarbeariaDbContext context)
        {
            _context = context;
        }

        public HorarioDisponivel? ObterPorBarbeiroDia(Guid barbeiroId, int diaSemana)
        {
            return _context.HorariosDisponiveis.FirstOrDefault(h => h.BarbeiroId == barbeiroId && h.DiaSemana == diaSemana);
        }
    }
}
