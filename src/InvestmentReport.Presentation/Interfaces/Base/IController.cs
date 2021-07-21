using System.Threading.Tasks;
using InvestmentReport.Presentation.Helpers;

namespace InvestmentReport.Presentation.Interfaces.Base
{

    public interface IController<TInput, TOutPut>
        where TInput : class
    {

        Task<HttpResponse<TOutPut>> action(HttpRequest<TInput> request);

    }

}