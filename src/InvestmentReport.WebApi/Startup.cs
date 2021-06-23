using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Microsoft.OpenApi.Models;

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API-INVESTMENT-REPORT",
                    Description = "Projeto hipotético de um back-end que retorna um lista consolidada dos investimentos de um dado cliente.",
                    Contact = new OpenApiContact
                    {
                        Name = "Felipe França",
                        Url = new Uri("https://www.linkedin.com/in/felipe-augusto-franca/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "GitHub",
                        Url = new Uri("https://github.com/macgyver1985/api-backend-to-investment-report"),
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });
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

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-INVESTMENT-REPORT V1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
