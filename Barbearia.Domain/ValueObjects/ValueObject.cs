namespace Barbearia.Domain.ValueObjects
{
    public abstract record ValueObject<TPrimitive>
    {
        public TPrimitive Value { get; }

        protected ValueObject(TPrimitive value)
        {
            Value = value;
        }

        public override string ToString() => Value?.ToString() ?? string.Empty;

        public static implicit operator TPrimitive(ValueObject<TPrimitive> vo) => vo.Value!;
    }
}
