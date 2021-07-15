using System;
using System.Threading;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Adapters;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace InvestmentReport.WebApi.Resources.HealthCheck
{

    /// <summary>
    /// Classe de verificação de saúde de qualquer implementação concreta do adapter ICache.
    /// </summary>
    /// <remarks>
    /// A instância dessa classe deve ser informada ao contexto da coleção de serviços da aplicação, exemplo:
    /// <code>
    /// services
    ///   .AddHealthChecks()
    ///   .AddCheck(
    ///       "Redis",
    ///       new CacheHealthCheck(new Redis(this.Configuration)),
    ///       HealthStatus.Unhealthy,
    ///       new string[] { "cache" }
    ///   );
    /// </code>
    /// </remarks>
    internal class CacheHealthCheck : IHealthCheck
    {
        private readonly ICache cacheAdapter;
        private const string CACHE_KEY = "health-check";
        private const string CACHE_TOTAL_TIME = "1.00:00:00";

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        /// <param name="cacheAdapter">Instância de quanquer implementação concreta do adapter ICache.</param>
        /// <see cref="ICache"/>
        public CacheHealthCheck(ICache cacheAdapter)
        {
            this.cacheAdapter = cacheAdapter;
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

                string responseCache = await this.cacheAdapter.Obtain(CACHE_KEY);

                if (string.IsNullOrEmpty(responseCache))
                    await this.cacheAdapter.Register(CACHE_KEY, "OK", TimeSpan.Parse(CACHE_TOTAL_TIME));
            }
            catch (Exception ex)
            {
                return new HealthCheckResult(status: context.Registration.FailureStatus, exception: ex);
            }

            return HealthCheckResult.Healthy();
        }
    }

}