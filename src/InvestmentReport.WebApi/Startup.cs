using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Adapters;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.Application.Service;
using InvestmentReport.CrossCutting.Trace;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.Infrastructure.Cache;
using InvestmentReport.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvestmentReport.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ILogger, LoggerInFile>();
            services.AddSingleton<ICache, Redis>();
            services.AddSingleton<IObtainAllInvestmentsHandler, ObtainAllInvestmentsHandler>();
            services.AddSingleton<IGetInvestments, GetInvestmentService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
