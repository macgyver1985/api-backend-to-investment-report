using InvestmentReport.Presentation.Interfaces.Base;
using InvestmentReport.Presentation.ViewModels;

namespace InvestmentReport.Presentation.Interfaces.Controllers
{

    public interface IObtainAllInvestmentsController
        : IController<object, ListInvestmentsModel>
    { }

}