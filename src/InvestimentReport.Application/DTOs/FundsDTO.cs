using System;
using Newtonsoft.Json;

namespace InvestimentReport.Application.DTOs
{

    public class FundsDTO
    {

        [JsonProperty("capitalInvestido")]
        public int InvestedCapital { get; set; }

        [JsonProperty("ValorAtual")]
        public int CurrentValue { get; set; }

        [JsonProperty("dataResgate")]
        public DateTime RedemptionDate { get; set; }

        [JsonProperty("dataCompra")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("iof")]
        public int IOF { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("totalTaxas")]
        public double OtherTaxes { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

    }

}