using Barbearia.Domain.Entities;
using Barbearia.Domain.Repositories;
using Barbearia.Domain.ValueObjects;
using Barbearia.Infrastructure.Persistence;

namespace Barbearia.Infrastructure.Repositories
{
	public class HorarioDisponivelRepository : BaseRepository<HorarioDisponivel>, IHorarioDisponivelRepository
    {
        public HorarioDisponivelRepository(BarbeariaDbContext context) : base(context) { }

        public HorarioDisponivel? ObterPorBarbeiroDia(BarbeiroId barbeiroId, int diaSemana)
        {
            return _context.HorariosDisponiveis.FirstOrDefault(h => h.BarbeiroId == barbeiroId && h.DiaSemana == diaSemana);
        }

        public List<HorarioDisponivel> ListarPorBarbeiro(BarbeiroId barbeiroId)
        { 
            return _context.HorariosDisponiveis.Where(wh => wh.BarbeiroId == barbeiroId).ToList();

        }
    }
}
