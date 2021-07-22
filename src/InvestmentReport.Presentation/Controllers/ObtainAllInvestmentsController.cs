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

    /// <summary>
    /// Classe que trata a requisição e a resposta HTTP referente ao relatório consolidado dos investimentos de uma dado cliente.
    /// </summary>
    /// <see cref="ControllerHelper"/>
    /// <see cref="IObtainAllInvestmentsController"/>
    public sealed class ObtainAllInvestmentsController
        : ControllerHelper, IObtainAllInvestmentsController
    {

        private readonly ILogger loggerAdapter;
        private readonly IObtainAllInvestmentsHandler obtainAllInvestmentsHandler;

        /// <summary>
        /// Construtor padrão da classe.
        /// </summary>
        /// <param name="loggerAdapter">Instância de qualquer classe que implemente a interface <see cref="ILogger"/>.</param>
        /// <param name="obtainAllInvestmentsHandler">Instância de qualquer classe que implemente a interface <see cref="IObtainAllInvestmentsHandler"/>.</param>
        public ObtainAllInvestmentsController(
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

        /// <summary>
        /// Método que trata a requisição e a resposta HTTP referente ao relatório consolidado dos investimentos do cliente.
        /// </summary>
        /// <param name="request">Instância de um <see cref="HttpRequest{TData}"/> onde TData é do tipo <see cref="object"/>.</param>
        /// <returns>Instância de um <see cref="HttpResponse{TData}"/> onde TData é do tipo <see cref="ListInvestmentsModel"/>.</returns>
        public async Task<HttpResponse<ListInvestmentsModel>> action(HttpRequest<object> request)
        {
            HttpResponse<ListInvestmentsModel> response = new HttpResponse<ListInvestmentsModel>();

            if (!Guid.TryParse(request.Headers["ProcessId"], out Guid processId))
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