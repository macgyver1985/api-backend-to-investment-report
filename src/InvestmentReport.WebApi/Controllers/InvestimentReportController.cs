using System;
using System.Linq;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.WebApi.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Controllers
{

    /// <summary>
    /// API que fornece relatórios consolidados dos investimentos dos clientes.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
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
        public async Task<OkObjectResult> Get()
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
                        CurrentValue = t.CurrentValue,
                        DueDate = t.DueDate,
                        InvestedValue = t.InvestedValue,
                        Name = t.Name,
                        RedemptionValue = t.RedemptionValue,
                        IrTax = t.Taxes.FirstOrDefault(tax => tax.Name == "IR").Value
                    };

                    result.Investments.Add(item);
                });

                return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
            }

            return null;
        }
    }
}
