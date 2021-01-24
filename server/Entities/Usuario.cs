using Dapper.Contrib.Extensions;
using FluentValidation;
using SmartAuth.ValueObjects;
using System;
using System.Linq;

namespace SmartAuth.Entities
{
    [Table("Usuario")]
    public class Usuario
    {
        [ExplicitKey]
        public Guid Id { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }

        // public Cpf Cpf { get; set; }

        public Usuario() { }

        public Usuario(string nome, string sobrenome, DateTime dataNascimento, string email, string rg, string cpf)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
            Email = email;
            Rg = rg;

            var cpfValidate = new Cpf(cpf).Validate();
            if (cpfValidate.Errors.Any())
                throw new ValidationException(cpfValidate.Errors);

            Cpf = cpf;
        }
    }
}
