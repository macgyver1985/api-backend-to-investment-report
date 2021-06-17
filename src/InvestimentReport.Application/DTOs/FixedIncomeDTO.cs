using System;
using Newtonsoft.Json;

namespace InvestimentReport.Application.DTOs
{
    public class FixedIncomeDTO
    {
        [JsonProperty("capitalInvestido")]
        public double InvestedCapital { get; set; }

        [JsonProperty("capitalAtual")]
        public double CurrentCapital { get; set; }

        [JsonProperty("quantidade")]
        public double Quantity { get; set; }

        [JsonProperty("vencimento")]
        public DateTime DueDate { get; set; }

        [JsonProperty("iof")]
        public double IOF { get; set; }

        [JsonProperty("outrasTaxas")]
        public double OtherTaxes { get; set; }

        [JsonProperty("taxas")]
        public double Taxes { get; set; }

        [JsonProperty("indice")]
        public string Index { get; set; }

        [JsonProperty("tipo")]
        public string Type { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("guarantidoFGC")]
        public bool GuaranteedByFGV { get; set; }

        [JsonProperty("dataOperacao")]
        public DateTime OperationDate { get; set; }

        [JsonProperty("precoUnitario")]
        public double UnitaryValue { get; set; }

        [JsonProperty("primario")]
        public bool Primary { get; set; }
    }
}