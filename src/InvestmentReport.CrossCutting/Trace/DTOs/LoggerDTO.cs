using System;
using Newtonsoft.Json;

namespace InvestmentReport.CrossCutting.Trace.DTOs
{

    /// <summary>
    /// Estrutura de dados para as informações que são persistidas pelo ILogger.
    /// </summary>
    public class LoggerDTO
    {

        /// <summary>
        /// Data de criação da mensagem.
        /// </summary>
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Indicação do tipo da mensagem.
        /// </summary>
        [JsonIgnore()]
        public ELoggerType Type { get; set; }

        /// <summary>
        /// Retorna o nome do tipo da mensagem.
        /// </summary>
        [JsonProperty("type")]
        public string TypeName
        {
            get
            {
                return Enum.GetName(typeof(ELoggerType), this.Type);
            }
            set
            {
                if (Enum.TryParse(typeof(ELoggerType), value, true, out object val))
                    this.Type = (ELoggerType)val;
            }
        }

        /// <summary>
        /// Guid de identificação do processo.
        /// </summary>
        [JsonProperty("processId")]
        public Guid ProcessId { get; set; }

        /// <summary>
        /// Nome do contexto que gerou a mensagem.
        /// </summary>
        [JsonProperty("context")]
        public string Context { get; set; }

        /// <summary>
        /// Nome do método que gerou a mensagem.
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// Texto descritivo da mensagem.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Instância de um ExceptionDTO.
        /// </summary>
        /// <see cref="InvestmentReport.CrossCutting.Trace.DTOs.ExceptionDTO"/>
        [JsonProperty("exception")]
        public ExceptionDTO Exception { get; set; }

        /// <summary>
        /// Dados que estão relacionados com a mensagem.
        /// </summary>
        [JsonProperty("payload")]
        public object Payload { get; set; }

    }

}