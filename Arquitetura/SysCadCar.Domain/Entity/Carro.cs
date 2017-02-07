using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SysCadCar.Domain.Enun;
using Newtonsoft.Json;

namespace SysCadCar.Domain.Entity
{
    public class Carro
    {
        [JsonProperty(PropertyName = "proprietario")]
        public string Proprietario { get; set; }

        [JsonProperty(PropertyName = "cpf")]
        public string CPF { get; set; }

        [JsonProperty(PropertyName = "placa")]
        public string Placa { get; set; }

        [JsonProperty(PropertyName = "renavan")]
        public string Renavan { get; set; }

        [JsonProperty(PropertyName = "urlFotos")]
        public string UrlFotos { get; set; }

        [JsonProperty(PropertyName = "bloqueio")]
        public Status Bloqueio { get; set; }

    }
}
