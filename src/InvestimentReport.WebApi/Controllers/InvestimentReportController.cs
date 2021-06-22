using System;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.Interfaces.Services;
using InvestimentReport.CrossCutting.Trace.Interfaces;
using InvestimentReport.WebApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InvestimentReport.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InvestimentReportController : ControllerBase
    {

        private readonly ILogger loggerAdapter;
        private readonly IObtainAllInvestimentsHandler obtainAllInvestimentsHandler;

        public InvestimentReportController(
            ILogger loggerAdapter,
            IObtainAllInvestimentsHandler obtainAllInvestimentsHandler
        )
        {
            this.loggerAdapter = loggerAdapter;
            this.obtainAllInvestimentsHandler = obtainAllInvestimentsHandler;
        }

        [HttpGet]
        public async Task<OkObjectResult> Get()
        {
            var temp = await this.obtainAllInvestimentsHandler
                .Execute(Guid.NewGuid());

            if (temp != null && temp.Any())
            {
                ListInvestimentsModel result = new ListInvestimentsModel();

                temp.ToList().ForEach(t =>
                {
                    var item = new InvestimentModel()
                    {
                        CurrentValue = t.CurrentValue,
                        DueDate = t.DueDate,
                        InvestedValue = t.InvestedValue,
                        Name = t.Name,
                        RedemptionValue = t.RedemptionValue,
                        IrTax = t.Taxes.FirstOrDefault(tax => tax.Name == "IR").Value
                    };

                    result.Investiments.Add(item);
                });

                return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
            }

            return null;
        }
    }
}
