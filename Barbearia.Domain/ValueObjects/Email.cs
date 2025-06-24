using System.Text.RegularExpressions;

namespace Barbearia.Domain.ValueObjects
{
    public sealed record Email : ValueObject<string>
    {
        public Email(string value) : base(value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Email não pode ser vazio.");

            var pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            if (!Regex.IsMatch(value, pattern))
                throw new ArgumentException("Formato de e-mail inválido.");

        }

        public static implicit operator string(Email email) => email.Value;
        public static implicit operator Email(string value) => new Email(value);
    }
}
