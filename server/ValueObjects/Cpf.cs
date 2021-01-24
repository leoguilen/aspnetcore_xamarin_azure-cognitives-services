using Ardalis.GuardClauses;
using FluentValidation.Results;
using SmartAuth.Validators;

namespace SmartAuth.ValueObjects
{
    public class Cpf : ValueObject
    {
        private bool _isValid;

        public Cpf(string value)
        {
            Guard.Against.NullOrEmpty(value, "Cpf");

            Value = value;
        }

        public string Value { get; }

        public override bool IsValid => _isValid;

        public override ValidationResult Validate()
        {
            var result = new CpfValidator().Validate(this);
            _isValid = result.IsValid;
            return result;
        }
    }
}
