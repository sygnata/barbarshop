namespace Barbearia.Application.DTOs.HorarioDisponivel
{
	public class HorarioDisponivelResponse
    {
        public Guid Id { get; set; }
        public Guid BarbeiroId { get; set; }
        public int DiaSemana { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFim { get; set; }
    }
}
