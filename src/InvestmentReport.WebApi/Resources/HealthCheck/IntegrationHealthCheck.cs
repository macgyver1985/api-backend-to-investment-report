using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RestSharp;

namespace InvestmentReport.WebApi.Resources.HealthCheck
{

    /// <summary>
    /// Classe de verificação de saúde das integrações HTTP.
    /// </summary>
    /// <remarks>
    /// A instância dessa classe deve ser informada ao contexto da coleção de serviços da aplicação, exemplo:
    /// <code>
    /// services
    ///   .AddHealthChecks()
    ///   .AddCheck(
    ///       "Direct Treasure",
    ///       new IntegrationHealthCheck(configuration["GetInvestmentService:DirectTreasure"], Method.GET),
    ///       HealthStatus.Unhealthy,
    ///       new string[] { "HTTP-SERVICES" }
    ///   );
    /// </code>
    /// </remarks>
    internal class IntegrationHealthCheck : IHealthCheck
    {
        private readonly string url;
        private readonly Method method;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        /// <param name="url">URL da integração que será verificada.</param>
        /// <param name="method">Método HTTP de requisição.</param>
        public IntegrationHealthCheck(string url, Method method)
        {
            this.url = url;
            this.method = method;
        }

        /// <summary>
        /// Método assíncrono que é executado pelo middleware de verificação de saúde.
        /// </summary>
        /// <param name="context">Instância de uma HealthCheckContext.</param>
        /// <param name="cancellationToken">Instância do token de cancelamento da verificação de saúde.</param>
        /// <returns>Retorna instância de uma Task de HealthCheckResult</returns>
        /// <see cref="HealthCheckContext"/>
        /// <see cref="HealthCheckResult"/>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                if (cancellationToken != null && cancellationToken.IsCancellationRequested)
                    cancellationToken.ThrowIfCancellationRequested();

                RestClient restClient = new RestClient(this.url);
                RestRequest restRequest = new RestRequest(this.method)
                {
                    RequestFormat = DataFormat.Json
                };

                restRequest.AddHeader("Accept", "application/json");
                restRequest.AddHeader("cache-control", "no-cache");

                IRestResponse response = await restClient.ExecuteAsync(restRequest);

                if (response.StatusCode != HttpStatusCode.OK)
                    return new HealthCheckResult(status: context.Registration.FailureStatus, response.Content);
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return HealthCheckResult.Healthy("Executing Successfully");
        }
    }

}