namespace Barbearia.Domain.ValueObjects
{
    public sealed record DescricaoBarbeiro : ValueObject<string>
    {
        public DescricaoBarbeiro(string value) : base(value)
        {
            if (value.Length > 500)
                throw new ArgumentException("Descrição muito longa", nameof(value));

        }

        public static implicit operator string(DescricaoBarbeiro descricao) => descricao.Value;
        public static implicit operator DescricaoBarbeiro(string value) => new(value);
    }
}
