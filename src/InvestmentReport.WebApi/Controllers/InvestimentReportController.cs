using System;
using System.Linq;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvestmentReport.WebApi.Controllers
{
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

        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            var temp = await this.obtainAllInvestmentsHandler
                .Execute(Guid.NewGuid());

            if (temp != null && temp.Any())
            {
                ListInvestmentsModel result = new ListInvestmentsModel();

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
