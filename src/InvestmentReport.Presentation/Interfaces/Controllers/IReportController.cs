using InvestmentReport.Presentation.Interfaces.Base;
using InvestmentReport.Presentation.ViewModels;

namespace InvestmentReport.Presentation.Interfaces.Controllers
{

    public interface IReportController
        : IController<object, ListInvestmentsModel>
    { }

}