using System.Collections.Generic;
using InvestimentReport.Application.Interfaces.Common;
using InvestimentReport.Domain;

namespace InvestimentReport.Application.Interfaces.Services
{

    public interface IObtainAllInvestimentsHandler : IHandlerWithoutCommand<IList<Investiment>> { }

}