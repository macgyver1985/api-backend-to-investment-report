using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InvestmentReport.WebApi.Controllers
{

    /// <summary>
    /// API que fornece relatórios consolidados dos investimentos dos clientes.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class InvestmentReportController : ControllerBase
    {

        private readonly ILogger loggerAdapter;
        private readonly IObtainAllInvestmentsHandler obtainAllInvestmentsHandler;

        public InvestmentReportController(
            ILogger loggerAdapter,
            IObtainAllInvestmentsHandler obtainAllInvestmentsHandler
        )
        {
            this.loggerAdapter = loggerAdapter;
            this.obtainAllInvestmentsHandler = obtainAllInvestmentsHandler;
        }

        /// <summary>
        /// Retorna o relatório consolidado dos investimentos do cliente.
        /// </summary>
        /// <returns>Instância de uma ListInvestmentsModel.</returns>
        /// <response code="200">Retorna a lista dos investimentos consolidados.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            var processId = Guid.NewGuid();

            var temp = await this.obtainAllInvestmentsHandler
                .Execute(processId);

            if (temp != null && temp.Any())
            {
                ListInvestmentsModel result = new ListInvestmentsModel()
                {
                    ProcessId = processId
                };

                temp.ToList().ForEach(t =>
                {
                    var item = new InvestmentModel()
                    {
                        ValorTotal = t.CurrentValue,
                        Vencimento = t.DueDate,
                        ValorInvestido = t.InvestedValue,
                        Nome = t.Name,
                        ValorResgate = t.RedemptionValue,
                        Ir = t.Taxes.FirstOrDefault(tax => tax.Name == "IR").Value
                    };

                    result.Investmentos.Add(item);
                });

                return new JsonResult(result, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }

            return null;
        }
    }
}
