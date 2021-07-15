using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace InvestmentReport.WebApi.Setup
{

    internal static class SwaggerSetup
    {

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
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

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API-INVESTMENT-REPORT V1");
            });
        }

    }

}