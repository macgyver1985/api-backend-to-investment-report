using System.Collections.Generic;
using InvestmentReport.Application.Interfaces.Common;
using InvestmentReport.Domain.Investments;

namespace InvestmentReport.Application.Interfaces.Services
{

    public interface IObtainAllInvestmentsHandler : IHandlerWithoutCommand<IList<Investment>> { }

}