namespace Barbearia.Application.DTOs.Servico
{
	public class ServicoResponse
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int DuracaoMinutos { get; set; }
        public decimal Preco { get; set; }
    }
}
