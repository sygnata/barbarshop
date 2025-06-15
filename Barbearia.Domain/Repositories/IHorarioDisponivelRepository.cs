using Barbearia.Domain.Entities;
using Barbearia.Domain.ValueObjects;

namespace Barbearia.Domain.Repositories
{
    public interface IHorarioDisponivelRepository
    {
        void Salvar();
        HorarioDisponivel? ObterPorBarbeiroDia(BarbeiroId barbeiroId, int diaSemana);
        void Adicionar(HorarioDisponivel horarioDisponivel);
        List<HorarioDisponivel> ListarPorBarbeiro(BarbeiroId barbeiroId);
    }

}
