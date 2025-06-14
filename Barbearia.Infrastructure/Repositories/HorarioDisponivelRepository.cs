using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class HorarioDisponivelRepository : BaseRepository<HorarioDisponivel>, IHorarioDisponivelRepository
    {
        public HorarioDisponivelRepository(BarbeariaDbContext context) : base(context) { }

        public HorarioDisponivel? ObterPorBarbeiroDia(Guid barbeiroId, int diaSemana)
        {
            return _context.HorariosDisponiveis.FirstOrDefault(h => h.BarbeiroId == barbeiroId && h.DiaSemana == diaSemana);
        }

        public List<HorarioDisponivel> ListarPorBarbeiro(Guid barbeiroId)
        { 
            return _context.HorariosDisponiveis.Where(wh => wh.BarbeiroId == barbeiroId).ToList();

        }
    }
}
