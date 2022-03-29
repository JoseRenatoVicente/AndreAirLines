using Newtonsoft.Json;

namespace AndreAirLines.Model.Entities
{
    public class Aeronave
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("capacidade")]
        public int Capacidade { get; set; }

        public override string ToString()
        {
            return $" Placa: {Id}\n Nome: {Nome}\n Capacidade: {Capacidade}";
        }
    }
}
