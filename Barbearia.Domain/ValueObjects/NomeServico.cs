namespace Barbearia.Domain.ValueObjects
{
    public sealed record NomeServico : ValueObject<string>
    {
        public NomeServico(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                throw new ArgumentException("Nome inválido", nameof(value));
  
        }

        public static implicit operator string(NomeServico nome) => nome.Value;
        public static implicit operator NomeServico(string value) => new(value);
    }
}
