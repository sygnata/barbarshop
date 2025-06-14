namespace Barbearia.Application.DTOs.Agendamento
{
	public class DisponibilidadeResponse
    {
        public IEnumerable<DateTime> HorariosDisponiveis { get; set; } = new List<DateTime>();
    }
}
