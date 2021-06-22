using System;
using Newtonsoft.Json;

namespace InvestimentReport.WebApi.ViewModels
{

    public class InvestimentModel
    {

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("valorInvestido")]
        public double InvestedValue { get; set; }

        [JsonProperty("valorTotal")]
        public double CurrentValue { get; set; }

        [JsonProperty("vencimento")]
        public DateTime DueDate { get; set; }

        [JsonProperty("Ir")]
        public double IrTax { get; set; }

        [JsonProperty("valorResgate")]
        public double RedemptionValue { get; set; }

    }

}