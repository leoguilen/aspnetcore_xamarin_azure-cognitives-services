using Newtonsoft.Json;
using System;

namespace SmartAuth.Models
{
    public class UsuarioModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public DateTime DataNascimento { get; set; }
        
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        public string Rg { get; set; }
        public string Cpf { get; set; }

        [JsonIgnore]
        public DateTime DataCadastro { get; set; }

    }
}
