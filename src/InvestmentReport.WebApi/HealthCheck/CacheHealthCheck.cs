using System;
using System.Threading;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Adapters;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace InvestmentReport.WebApi.HealthCheck
{

    public class CacheHealthCheck : IHealthCheck
    {
        private readonly ICache cacheAdapter;
        private const string CACHE_KEY = "health-check";
        private const string CACHE_TOTAL_TIME = "1.00:00:00";

        public CacheHealthCheck(ICache cacheAdapter)
        {
            this.cacheAdapter = cacheAdapter;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
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