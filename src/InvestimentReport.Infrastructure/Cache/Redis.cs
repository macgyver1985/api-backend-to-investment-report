using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using InvestimentReport.Application.Interfaces.Adapters;
using StackExchange.Redis;
using InvestimentReport.Application.Helper;

namespace InvestimentReport.Infrastructure.Cache
{

    public sealed class Redis : CacheHelper, ICache
    {

        private readonly ConnectionMultiplexer connection;

        public Redis(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.");

            this.connection = ConnectionMultiplexer.Connect(
                configuration.GetConnectionString("Redis")
            );
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.connection.Close();
                this.connection.Dispose();
            }

            disposed = true;
        }

        public async Task<string> Obtain(string key)
        {
            IDatabase db = null;

            lock (this.synchronizeTasks)
            {
                db = this.connection
                    .GetDatabase();
            }

            var result = await db.StringGetAsync(key);

            return result;
        }

        public async Task<bool> Register(string key, string value, TimeSpan expire)
        {
            IDatabase db = null;

            lock (this.synchronizeTasks)
            {
                db = this.connection
                    .GetDatabase();
            }

            await db.StringSetAsync(key, value);

            await db.KeyExpireAsync(key, expire);

            return true;
        }

        public async Task Remove(string key)
        {
            IDatabase db = null;

            lock (this.synchronizeTasks)
            {
                db = this.connection
                    .GetDatabase();
            }

            await db.KeyDeleteAsync(key);
        }

    }

}