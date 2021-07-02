using System;
using System.Linq;
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
        /// <param name="processId">
        /// Guid que deve ser passado pelo header da request para identificar o processo.
        /// </param>
        /// <returns>Instância de uma ListInvestmentsModel.</returns>
        /// <response code="200">Retorna a lista dos investimentos consolidados.</response>
        /// <response code="204">Não foram encontrados investimentos.</response>
        /// <response code="500">Erro interno de processamento.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ListInvestmentsModel>> Get([FromHeader] Guid processId)
        {
            var temp = await this.obtainAllInvestmentsHandler
                .Execute(processId);

            if (temp == null || !temp.Any())
                return NoContent();

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

            return result;
        }
    }
}
