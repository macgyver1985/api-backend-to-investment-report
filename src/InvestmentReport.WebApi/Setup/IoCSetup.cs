using InvestmentReport.Application.Interfaces.Adapters;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.Application.Service;
using InvestmentReport.CrossCutting.Trace;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.Infrastructure.Cache;
using InvestmentReport.Infrastructure.Services;
using InvestmentReport.Presentation.Controllers;
using InvestmentReport.Presentation.Interfaces.Controllers;
using InvestmentReport.WebApi.Resources.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InvestmentReport.WebApi.Setup
{

    internal static class IoCSetup
    {

        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ILogger, LoggerInFile>();
            services.AddSingleton<ICache, Redis>();
            services.AddScoped<IObtainAllInvestmentsHandler, ObtainAllInvestmentsHandler>();
            services.AddScoped<IGetInvestments, GetInvestmentService>();
            services.AddScoped<IReportController, ReportController>();
            services.AddScoped<ResponseFilter>();
        }

    }

}