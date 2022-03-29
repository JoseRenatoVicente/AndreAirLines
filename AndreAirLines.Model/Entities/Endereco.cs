using Newtonsoft.Json;
using System;

namespace AndreAirLines.Model.Entities
{
    public class Endereco
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("bairro")]
        public string Bairro { get; set; }
        [JsonProperty("cidade")]
        public string Cidade { get; set; }
        [JsonProperty("pais")]
        public string Pais { get; set; }
        [JsonProperty("cep")]
        public string CEP { get; set; }
        [JsonProperty("logradouro")]
        public string Logradouro { get; set; }
        [JsonProperty("estado")]
        public string Estado { get; set; }
        [JsonProperty("numero")]
        public string Numero { get; set; }
        [JsonProperty("complemento")]
        public string Complemento { get; set; }

        public override string ToString()
        {
            return $"Logradouro: {Logradouro}\n Número: {Numero}\n Cidade: {Cidade}\n Estado: {Estado}\n País: {Pais}\n Bairro: {Bairro}\n Complemento: {Complemento}";
        }
    }
}
