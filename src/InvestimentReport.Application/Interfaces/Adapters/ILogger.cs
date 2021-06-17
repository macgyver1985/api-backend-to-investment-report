using System;
using System.Threading.Tasks;

namespace InvestimentReport.Application.Interfaces.Adapters
{

    public interface ILogger : IDisposable
    {

        Task Error<Context>(Guid processId, Exception ex, string payload = null);

        Task Debug<Context>(Guid processId, string payload);

    }

}