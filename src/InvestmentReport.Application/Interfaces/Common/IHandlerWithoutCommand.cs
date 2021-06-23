using System;
using System.Threading.Tasks;

namespace InvestmentReport.Application.Interfaces.Common
{

    public interface IHandlerWithoutCommand<T> : IDisposable
    {

        Task<T> Execute(Guid processId);

    }

}