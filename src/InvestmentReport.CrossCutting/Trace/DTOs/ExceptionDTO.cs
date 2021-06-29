using Newtonsoft.Json;

namespace InvestmentReport.CrossCutting.Trace.DTOs
{

    /// <summary>
    /// Estrutura de dados para o erro da mensagem.
    /// </summary>
    public class ExceptionDTO
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
        /// Texto descritido do erro.
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