using Newtonsoft.Json;
using System;

namespace SmartAuth.Models
{
    public class Usuario
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("sobrenome")]
        public string Sobrenome { get; set; }

        [JsonProperty("dataNascimento")]
        public DateTime DataNascimento { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("rg")]
        public string Rg { get; set; }

        [JsonProperty("cpf")]
        public string Cpf { get; set; }

        [JsonProperty("dataCadastro")]
        public DateTime DataCadastro { get; set; }
    }
}
