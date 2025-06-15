namespace Barbearia.Domain.ValueObjects
{
    public sealed record TenantId : ValueObject<Guid>
    {
        public TenantId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("TenantId não pode ser vazio.");
        }

        public static implicit operator Guid(TenantId id) => id.Value;
        public static implicit operator TenantId(Guid value) => new TenantId(value);
    }
}
