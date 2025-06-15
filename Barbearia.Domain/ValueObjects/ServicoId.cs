namespace Barbearia.Domain.ValueObjects
{
	public sealed record ServicoId : ValueObject<Guid>
    {
        public ServicoId(Guid value) : base(value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("ServicoId não pode ser vazio.");

        }

        public static implicit operator Guid(ServicoId id) => id.Value;
        public static implicit operator ServicoId(Guid value) => new ServicoId(value);
    }
}
