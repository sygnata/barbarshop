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
            return _context.HorariosDisponiveis.FirstOrDefault(h => h.BarbeiroId == barbeiroId && h.DiaSemana == diaSemana && h.Ativo);
        }

        public List<HorarioDisponivel> ListarPorBarbeiro(BarbeiroId barbeiroId)
        { 
            return _context.HorariosDisponiveis.Where(wh => wh.BarbeiroId == barbeiroId && wh.Ativo).ToList();

        }

        public bool ExisteConflitoHorario(BarbeiroId barbeiroId, int diaSemana, TimeSpan horaInicio, TimeSpan horaFim)
        {
            return _context.HorariosDisponiveis
                .Any(h =>
                    h.BarbeiroId == barbeiroId &&
                    h.DiaSemana == diaSemana &&
                    horaInicio < h.HoraFim &&
                    horaFim > h.HoraInicio);
        }
    }
}
