using System.Text.RegularExpressions;

namespace Barbearia.Domain.ValueObjects
{
	public sealed record Telefone : ValueObject<string>
    {
        public Telefone(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Telefone não pode ser vazio.");

            //var pattern = @"^(?:\+55\s?)?(?:\(?[1-9][0-9]\)?\s?)?(?:9?[0-9]{4}\-?[0-9]{4})$";  // ajuste conforme regras do Brasil

            //if (!Regex.IsMatch(value, pattern))
            //    throw new ArgumentException("Telefone inválido.");
        }

        public static implicit operator string(Telefone telefone) => telefone.Value;
        public static implicit operator Telefone(string value) => new Telefone(value);
    }
}
