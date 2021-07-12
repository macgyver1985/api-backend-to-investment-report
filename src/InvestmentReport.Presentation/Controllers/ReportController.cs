using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.Presentation.Helpers;
using InvestmentReport.Presentation.Interfaces.Controllers;
using InvestmentReport.Presentation.Resources;
using InvestmentReport.Presentation.ViewModels;

namespace InvestmentReport.Presentation.Controllers
{

    public sealed class ReportController
        : ControllerHelper, IReportController
    {

        private readonly ILogger loggerAdapter;
        private readonly IObtainAllInvestmentsHandler obtainAllInvestmentsHandler;

        public ReportController(
            ILogger loggerAdapter,
            IObtainAllInvestmentsHandler obtainAllInvestmentsHandler
        )
        {
            if (loggerAdapter == null)
                throw new ArgumentException("Logger adapter is null.");

            if (obtainAllInvestmentsHandler == null)
                throw new ArgumentException("ObtainAllInvestmentsHandler is null.");

            this.loggerAdapter = loggerAdapter;
            this.obtainAllInvestmentsHandler = obtainAllInvestmentsHandler;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.obtainAllInvestmentsHandler.Dispose();
            }

            disposed = true;
        }

        public async Task<HttpResponse<ListInvestmentsModel>> action(HttpRequest<object> request)
        {
            HttpResponse<ListInvestmentsModel> response = new HttpResponse<ListInvestmentsModel>();

            if (Guid.TryParse(request.Headers["ProcessId"].ToString(), out Guid processId))
                processId = Guid.NewGuid();

            try
            {
                var temp = await this.obtainAllInvestmentsHandler
                    .Execute(processId);

                if (temp == null || !temp.Any())
                    response.StatusCode = HttpStatusCode.NoContent;
                else
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Success = new ListInvestmentsModel()
                    {
                        Investments = Mapping.Mapper.Map<IList<InvestmentModel>>(temp)
                    };
                }
            }
            catch (Exception ex)
            {
                this.loggerAdapter
                    .Error(
                        processId,
                        "Exceção ao tentar obter todos os investimentos.",
                        ex
                    );

                response.StatusCode = HttpStatusCode.InternalServerError;
                response.Fault = ex.Message;
            }
            finally
            {
                response.ProcessId = processId;
            }

            return response;
        }

    }

}