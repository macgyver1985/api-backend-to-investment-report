using System;
using System.Threading.Tasks;

namespace InvestimentReport.Application.Interfaces.Common
{

    public interface IHandlerWithoutCommand<T> : IDisposable
    {

        Task<T> Execute(Guid processId);

    }

}