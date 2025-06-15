namespace Barbearia.Domain.ValueObjects
{
	public record ServicoId
    {
        public Guid Value { get; }

        public ServicoId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ServicoId não pode ser vazio.");

            Value = value;
        }

        public static implicit operator Guid(ServicoId id) => id.Value;
        public static implicit operator ServicoId(Guid value) => new ServicoId(value);
    }
}
