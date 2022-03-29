using Newtonsoft.Json;
using System;

namespace AndreAirLines.Model.Entities
{
    public class Passagem
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("voo")]
        public Voo Voo { get; set; }
        [JsonProperty("passageiro")]
        public Passageiro Passageiro { get; set; }
    }
}
