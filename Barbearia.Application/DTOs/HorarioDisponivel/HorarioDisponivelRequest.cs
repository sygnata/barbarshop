namespace Barbearia.Application.DTOs.HorarioDisponivel
{
	public class HorarioDisponivelRequest
    {
        public Guid BarbeiroId { get; set; }
        public int DiaSemana { get; set; } // 0=Domingo, 6=Sábado
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }
}
