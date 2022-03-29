using Newtonsoft.Json;

namespace AndreAirLines.Model.Entities
{
    public class Aeroporto
    {
        [JsonProperty("sigla")]
        public string Sigla { get; set; }
        [JsonProperty("nome")]
        public string Nome { get; set; }
        [JsonProperty("endereco")]
        public Endereco Endereco { get; set; }

        public override string ToString()
        {
            return $" Sigla: {Sigla}\n Nome: {Nome}\n {(Endereco == null ? "" : "Endereço: " + Endereco.ToString()) }";
        }
    }
}

