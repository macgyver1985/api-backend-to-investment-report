using InvestmentReport.WebApi.Resources.Filters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentReport.WebApi.Setup
{

    internal static class ControllersSetup
    {

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddControllers(opt => opt.Filters.Add(typeof(ResponseFilter)))
                .AddNewtonsoftJson();
        }

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }

}