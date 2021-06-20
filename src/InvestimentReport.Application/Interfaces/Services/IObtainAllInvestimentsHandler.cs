using System.Collections.Generic;
using InvestimentReport.Application.Interfaces.Common;
using InvestimentReport.Domain.Investiments;

namespace InvestimentReport.Application.Interfaces.Services
{

    public interface IObtainAllInvestimentsHandler : IHandlerWithoutCommand<IList<Investiment>> { }

}