using Barbearia.Domain.Entities;

namespace Barbearia.Domain.Repositories
{
    public interface IHorarioDisponivelRepository
    {
        void Salvar();
        HorarioDisponivel? ObterPorBarbeiroDia(Guid barbeiroId, int diaSemana);
        void Adicionar(HorarioDisponivel horarioDisponivel);
        List<HorarioDisponivel> ListarPorBarbeiro(Guid barbeiroId);
    }

}
