namespace Barbearia.Domain.ValueObjects
{
	public readonly struct BarbeiroId
    {
        public Guid Value { get; }

        public BarbeiroId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("BarbeiroId não pode ser vazio.");

            Value = value;
        }

        public static implicit operator Guid(BarbeiroId id) => id.Value;
        public static implicit operator BarbeiroId(Guid value) => new BarbeiroId(value);
    }
}
