using System;
using System.Threading.Tasks;

namespace InvestimentReport.CrossCutting.Trace.Interfaces
{

    public interface ILogger : IDisposable
    {

        bool IsLoggerEnable { get; }

        bool IsError { get; }

        bool IsDebug { get; }

        Task<ILogger> Error<Context, TPayload>(Guid processId, string message, Exception ex, TPayload payload);

        Task<ILogger> Error<Context>(Guid processId, string message, Exception ex);

        Task<ILogger> Debug<Context, TPayload>(Guid processId, string message, TPayload payload);

    }

}