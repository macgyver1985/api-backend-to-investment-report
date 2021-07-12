using System.Threading.Tasks;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.Presentation.Helpers;
using InvestmentReport.Presentation.Interfaces.Controllers;
using InvestmentReport.Presentation.ViewModels;
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
        private readonly IReportController reportController;

        public InvestmentReportController(
            ILogger loggerAdapter,
            IReportController reportController
        )
        {
            this.loggerAdapter = loggerAdapter;
            this.reportController = reportController;
        }

        /// <summary>
        /// Retorna o relatório consolidado dos investimentos do cliente.
        /// </summary>
        /// <returns>Instância de uma ListInvestmentsModel.</returns>
        /// <response code="200">Retorna a lista dos investimentos consolidados.</response>
        /// <response code="204">Não foram encontrados investimentos.</response>
        /// <response code="500">Erro interno de processamento.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<HttpResponse<ListInvestmentsModel>>> Get()
        {
            var response = await this.reportController
                .action(new HttpRequest<object>(this.Request.Headers));

            return response;
        }

    }

}
