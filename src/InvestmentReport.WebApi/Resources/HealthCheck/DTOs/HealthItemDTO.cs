using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Resources.HealthCheck.DTOs
{

    /// <summary>
    /// Estrutura de dados que contém as informações de saúde de um serviço.
    /// </summary>
    internal class HealthItemDTO
    {

        /// <summary>
        /// Nome do serviço que foi verificado.
        /// </summary>
        [JsonProperty("serviceName")]
        public string ServiceName { get; set; }

        /// <summary>
        /// Tempo que levou para concluir a verificação de saúde.
        /// </summary>
        [JsonProperty("duration")]
        public TimeSpan Duration { get; set; }

        /// <summary>
        /// Status da saúde do serviço.
        /// </summary>
        /// <see cref="HealthStatus"/>
        [JsonIgnore]
        public HealthStatus Status { get; set; }

        /// <summary>
        /// Descrição do status da saúde do serviço.
        /// </summary>
        /// <see cref="HealthStatus"/>
        [JsonProperty("status")]
        public string StatusDescription
        {
            get
            {
                return Enum.GetName(typeof(HealthStatus), this.Status);
            }
            set
            {
                if (Enum.TryParse(typeof(HealthStatus), value, true, out object val))
                    this.Status = (HealthStatus)val;
            }
        }

        /// <summary>
        /// Lista de dados vinculado verificação de saúde.
        /// </summary>
        [JsonProperty("data")]
        public IDictionary<string, object> Data { get; set; }

        /// <summary>
        /// Exceção vinculada a verificação de saúde quando o Status é diferente Healthy.
        /// </summary>
        /// <see cref="HealthExceptionDTO"/>
        [JsonProperty("exception")]
        public HealthExceptionDTO Exception { get; set; }

        /// <summary>
        /// Marcação de classificação do serviço que está sendo verificado.
        /// </summary>
        [JsonProperty("tags")]
        public IEnumerable Tags { get; set; }

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public HealthItemDTO()
        {
            this.Data = new Dictionary<string, object>();
        }

    }

}