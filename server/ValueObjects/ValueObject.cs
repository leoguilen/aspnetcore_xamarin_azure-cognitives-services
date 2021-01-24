using FluentValidation.Results;

namespace SmartAuth.ValueObjects
{
    public abstract class ValueObject
    {
        public abstract bool IsValid { get; }
        public abstract ValidationResult Validate();
    }
}
