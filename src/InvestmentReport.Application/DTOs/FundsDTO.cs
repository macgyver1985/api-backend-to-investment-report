using System;
using Newtonsoft.Json;

namespace InvestmentReport.Application.DTOs
{

    /// <summary>
    /// DTO usado para transferir os dados do serviços que devolve investimentos
    /// do tipos fundos para a camada de application.
    /// </summary>
    public class FundsDTO
    {

        [JsonProperty("capitalInvestido")]
        public double InvestedCapital { get; set; }

        [JsonProperty("ValorAtual")]
        public double CurrentValue { get; set; }

        [JsonProperty("dataResgate")]
        public DateTime RedemptionDate { get; set; }

        [JsonProperty("dataCompra")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("iof")]
        public double IOF { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

        [JsonProperty("totalTaxas")]
        public double TotalTaxes { get; set; }

        [JsonProperty("quantity")]
        public double Quantity { get; set; }

    }

}