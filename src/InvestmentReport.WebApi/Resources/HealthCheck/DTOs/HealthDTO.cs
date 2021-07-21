using System;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Resources.HealthCheck.DTOs
{

    /// <summary>
    /// Estrutura de dados que centraliza o resultado da verificação de saúde dos serviços configurados.
    /// </summary>
    internal class HealthDTO
    {

        /// <summary>
        /// Total de tempo decorrido para verificar todos os serviços configurados.
        /// </summary>
        [JsonProperty("totalDuration")]
        public TimeSpan TotalDuration { get; set; }

        /// <summary>
        /// Status geral da saúde de todos os serviços configurados.
        /// </summary>
        /// <see cref="HealthStatus"/>
        [JsonIgnore]
        public HealthStatus Status { get; set; }

        /// <summary>
        /// Descrição do status geral da saúde de todos os serviços configurados.
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
        /// Lista do relatório de saúde de todos os serviços configurados.
        /// </summary>
        /// <see cref="HealthItemDTO"/>
        [JsonProperty("items")]
        public IList<HealthItemDTO> HealthItems { get; set; }

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        public HealthDTO()
        {
            this.HealthItems = new List<HealthItemDTO>();
        }

    }

}