using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentReport.Application.Interfaces.Adapters
{

    public interface IGetInvestments : IDisposable
    {

        Task<IList<T>> GetOf<T>(Guid processId);

    }

}