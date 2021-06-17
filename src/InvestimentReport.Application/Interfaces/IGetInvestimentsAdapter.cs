using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestimentReport.Application.Interfaces
{
    public interface IGetInvestimentsAdapter
    {
        Task<IList<T>> GetOf<T>();
    }
}