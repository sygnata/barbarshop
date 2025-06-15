namespace Barbearia.Domain.ValueObjects
{
	public readonly struct TenantId
    {
        public Guid Value { get; }

        public TenantId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("TenantId não pode ser vazio.");

            Value = value;
        }

        public static implicit operator Guid(TenantId id) => id.Value;
        public static implicit operator TenantId(Guid value) => new TenantId(value);
    }
}
