using InvestmentReport.Infrastructure.Cache;
using InvestmentReport.WebApi.Resources.HealthCheck;
using InvestmentReport.WebApi.Resources.HealthCheck.UI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace InvestmentReport.WebApi.Setup
{

    internal static class HealthCheckSetup
    {

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddHealthChecks()
                .AddCheck(
                    "Redis",
                    new CacheHealthCheck(new Redis(configuration)),
                    HealthStatus.Unhealthy,
                    new string[] { "cache" }
                );
        }

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseHealthChecks("/hc", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = HealthCheckResponse.Json
            });
        }

    }

}