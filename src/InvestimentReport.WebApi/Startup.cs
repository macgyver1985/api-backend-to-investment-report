using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.Interfaces.Adapters;
using InvestimentReport.Application.Interfaces.Services;
using InvestimentReport.Application.Service;
using InvestimentReport.CrossCutting.Trace;
using InvestimentReport.CrossCutting.Trace.Interfaces;
using InvestimentReport.Infrastructure.Cache;
using InvestimentReport.Infrastructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InvestimentReport.WebApi
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
            services.AddSingleton<IObtainAllInvestimentsHandler, ObtainAllInvestimentsHandler>();
            services.AddSingleton<IGetInvestiments, GetInvestimentService>();
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
