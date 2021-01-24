using FluentValidation;
using SmartAuth.Entities;

namespace SmartAuth.Validators
{
    public class UsuarioValidator : AbstractValidator<Usuario>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nome)
                .Must(s => !string.IsNullOrEmpty(s))
                .WithMessage("Nome do usuário não pode ser nulo ou vazio");
            RuleFor(x => x.Sobrenome)
                .Must(s => !string.IsNullOrEmpty(s))
                .WithMessage("Sobrenome do usuário não pode ser nulo ou vazio");
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("Endereço de email é inválido");
        }
    }
}
