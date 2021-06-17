using System;
using System.Threading.Tasks;

namespace InvestimentReport.Application.Interfaces
{
    public interface ICacheAdapter : IDisposable
    {
        Task<bool> Register(string key, string value, TimeSpan expire);

        Task<string> Obtain(string key);

        Task Remove(string key);
    }
}