using System;
using Newtonsoft.Json;

namespace InvestmentReport.Application.DTOs
{

    /// <summary>
    /// DTO usado para transferir os dados do serviços que devolve investimentos
    /// do tipos tesouro direto para a camada de application.
    /// </summary>
    public class DirectTreasureDTO
    {

        [JsonProperty("valorInvestido")]
        public double InvestedValue { get; set; }

        [JsonProperty("valorTotal")]
        public double Amount { get; set; }

        [JsonProperty("vencimento")]
        public DateTime DueDate { get; set; }

        [JsonProperty("dataDeCompra")]
        public DateTime PurchaseDate { get; set; }

        [JsonProperty("iof")]
        public int IOF { get; set; }

        [JsonProperty("indice")]
        public string Index { get; set; }

        [JsonProperty("tipo")]
        public string Type { get; set; }

        [JsonProperty("nome")]
        public string Name { get; set; }

    }

}