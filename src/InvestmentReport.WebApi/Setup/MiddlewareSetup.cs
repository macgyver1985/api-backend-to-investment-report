using System;
using System.Collections.Generic;
using System.Net;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.Presentation.Helpers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Setup
{

    internal static class MiddlewareSetup
    {

        public static void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.Use(async (context, next) =>
            {
                if (!Guid.TryParse(context.Request.Headers["ProcessId"], out Guid processId) &&
                    !context.Request.Headers.TryAdd("ProcessId", Guid.NewGuid().ToString()))
                    context.Request.Headers["ProcessId"] = Guid.NewGuid().ToString();

                await next.Invoke();
            });

            // if (env.IsDevelopment() || env.IsEnvironment("Local"))
            //     app.UseDeveloperExceptionPage();
            // else
            app.UseExceptionHandler(option => option.Run(async context =>
            {
                if (!Guid.TryParse(context.Request.Headers["ProcessId"], out Guid processId))
                    processId = Guid.NewGuid();

                var logger = context.RequestServices.GetService<ILogger>();
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var result = new HttpResponse<object>()
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Fault = exceptionHandlerPathFeature.Error.Message,
                    ProcessId = processId
                };

                logger.Error(
                    processId,
                    "Erro interceptado no manipulador de exceção.",
                    exceptionHandlerPathFeature.Error,
                    new
                    {
                        controller = exceptionHandlerPathFeature.Path
                    }
                );

                context.Response.StatusCode = (int)result.StatusCode;
                context.Response.ContentType = "application/json";

                await context.Response.WriteAsync(JsonConvert.SerializeObject(result, Formatting.Indented));
            }));
        }

    }

}