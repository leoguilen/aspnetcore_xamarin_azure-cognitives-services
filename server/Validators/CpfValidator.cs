using FluentValidation;
using SmartAuth.ValueObjects;

namespace SmartAuth.Validators
{
    public class CpfValidator : AbstractValidator<Cpf>
    {
        public CpfValidator()
        {
            RuleFor(x => x.Value)
                .Length(11, 14)
                .WithMessage("Valor de CPF deve conter entre 11 e 14 caracteres");
            RuleFor(x => x.Value)
                .Must(x => IsValidCpf(x))
                .WithMessage("Valor de CPF não é válido")
                .When(x => x.Value.Length >= 11 && x.Value.Length <= 14, ApplyConditionTo.CurrentValidator);
        }

        private bool IsValidCpf(string cpf)
        {
            var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return false;
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (var i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;
            for (var i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto;
            return cpf.EndsWith(digito);
        }
    }
}
