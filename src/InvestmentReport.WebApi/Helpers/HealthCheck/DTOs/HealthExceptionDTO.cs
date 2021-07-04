using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Helpers.HealthCheck.DTOs
{

    /// <summary>
    /// Estrutura de dados para o erro do health check.
    /// </summary>
    public class HealthExceptionDTO
    {

        /// <summary>
        /// Pilha da ocorrência do erro.
        /// </summary>
        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }

        /// <summary>
        /// Local de origem do erro.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Descrição do erro.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Tipo da erro.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

    }

}