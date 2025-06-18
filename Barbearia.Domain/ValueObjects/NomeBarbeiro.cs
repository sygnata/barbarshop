namespace Barbearia.Domain.ValueObjects
{
    public sealed record NomeBarbeiro : ValueObject<string>
    {
        public NomeBarbeiro(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                throw new ArgumentException("Nome inválido", nameof(value));
  
        }

        public static implicit operator string(NomeBarbeiro nome) => nome.Value;
        public static implicit operator NomeBarbeiro(string value) => new(value);
    }
}
