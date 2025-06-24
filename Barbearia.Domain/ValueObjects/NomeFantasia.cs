namespace Barbearia.Domain.ValueObjects
{
    public sealed record NomeFantasia : ValueObject<string>
    {
        public NomeFantasia(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("NomeFantasia inválido.");
        }

        // 🔑 Construtor parameterless para o EF Core
        private NomeFantasia() : base(string.Empty) { }
    }
}
