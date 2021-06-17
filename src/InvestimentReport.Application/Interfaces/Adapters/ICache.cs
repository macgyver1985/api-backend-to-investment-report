using System;
using System.Threading.Tasks;

namespace InvestimentReport.Application.Interfaces.Adapters
{

    public interface ICache : IDisposable
    {

        Task<bool> Register(string key, string value, TimeSpan expire);

        Task<string> Obtain(string key);

        Task Remove(string key);

    }

}