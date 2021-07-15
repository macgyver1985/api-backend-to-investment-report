using InvestmentReport.WebApi.Setup;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentReport.WebApi
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IoCSetup.ConfigureServices(services, this.Configuration);

            ControllersSetup.ConfigureServices(services, this.Configuration);

            SwaggerSetup.ConfigureServices(services, this.Configuration);

            HealthCheckSetup.ConfigureServices(services, this.Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            MiddlewareSetup.ConfigureApplication(app, env, this.Configuration);

            AuthorizationSetup.ConfigureApplication(app, env, this.Configuration);

            ControllersSetup.ConfigureApplication(app, env, this.Configuration);

            SwaggerSetup.ConfigureApplication(app, env, this.Configuration);

            HealthCheckSetup.ConfigureApplication(app, env, this.Configuration);
        }

    }

}
