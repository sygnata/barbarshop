using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IHorarioDisponivelRepository
    {
        HorarioDisponivel? ObterPorBarbeiroDia(Guid barbeiroId, int diaSemana);
    }

}
