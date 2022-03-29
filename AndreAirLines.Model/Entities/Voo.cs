using Newtonsoft.Json;
using System;

namespace AndreAirLines.Model.Entities
{
    public class Voo
    {
        [JsonProperty("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonProperty("destino")]
        public Aeroporto Destino { get; set; }
        [JsonProperty("origem")]
        public Aeroporto Origem { get; set; }
        [JsonProperty("aeronave")]
        public Aeronave Aeronave { get; set; }
        [JsonProperty("horarioEmbarque")]
        public DateTime HorarioEmbarque { get; set; }
        [JsonProperty("horarioDesembarque")]
        public DateTime HorarioDesembarque { get; set; }


        public override string ToString()
        {
            return $"Destino:\n{Destino.ToString()}\nOrigem:\n{Origem.ToString()}\nAeronave:\n{Aeronave.ToString()}\n Horario de Embarque: {HorarioEmbarque.ToString("dd/MM/yyyy mm:ss")}\n Horario de Desembarque: {HorarioDesembarque.ToString("dd/MM/yyyy mm:ss")}";
        }
    }
}
