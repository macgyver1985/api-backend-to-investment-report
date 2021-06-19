using System;
using System.Threading.Tasks;

namespace InvestimentReport.CrossCutting.Interfaces
{

    public interface ILogger : IDisposable
    {

        Task Error<Context>(Guid processId, string message, Exception ex, string payload = null);

        Task Debug<Context>(Guid processId, string message, string payload);

    }

}