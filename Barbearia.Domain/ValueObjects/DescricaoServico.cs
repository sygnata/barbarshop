namespace Barbearia.Domain.ValueObjects
{
    public sealed record DescricaoServico : ValueObject<string>
    {
        public DescricaoServico(string value) : base(value)
        {
            if (value.Length > 500)
                throw new ArgumentException("Descrição muito longa", nameof(value));

        }

        public static implicit operator string(DescricaoServico descricao) => descricao.Value;
        public static implicit operator DescricaoServico(string value) => new(value);
    }
}
