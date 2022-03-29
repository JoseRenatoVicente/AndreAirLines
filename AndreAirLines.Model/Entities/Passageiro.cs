using AndreAirLines.Model.Entities.Enums;
using AndreAirLines.Model.Entities.Helpers;
using Newtonsoft.Json;
using System;

namespace AndreAirLines.Model.Entities
{
    public class Passageiro
    {
        [JsonProperty("cpf")]
        public string Cpf { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("telefone")]
        public string Telefone { get; set; }
        [JsonProperty("sexo")]
        public Sexo Sexo { get; set; }
        [JsonProperty("dataNasc")]
        public DateTime DataNasc { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        public bool ValidarCpf()
        {
            return ValidarCpf(Cpf);
        }
        public bool ValidarCpf(string cpf)
        {
            return ValidaCPF.IsCpf(cpf);
        }

        public bool ValidarEmail()
        {
            return ValidarEmail(Email);
        }

        public bool ValidarEmail(string email)
        {
            return RegexUtilities.IsValidEmail(email);
        }

        public override string ToString()
        {
            return $" Nome: {Nome}\n CPF: {Cpf}\n Telefone: {Telefone}\n Sexo:{Sexo}\n Data de Nascimento: {DataNasc.ToString("dd/MM/yyy")}\n Email: {Email}\n {(Endereco == null ? "" : "Endereço: " + Endereco.ToString()) }";
        }
    }
}
