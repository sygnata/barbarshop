namespace Barbearia.Domain.ValueObjects
{
	public sealed record BarbeiroId : ValueObject<Guid>
    {
        public BarbeiroId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("BarbeiroId não pode ser vazio.");
        }

        public static implicit operator Guid(BarbeiroId id) => id.Value;
        public static implicit operator BarbeiroId(Guid value) => new BarbeiroId(value);
    }
}
