using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestimentReport.Application.Interfaces.Adapters
{

    public interface IGetInvestiments : IDisposable
    {

        Task<IList<T>> GetOf<T>(Guid processId);

    }

}