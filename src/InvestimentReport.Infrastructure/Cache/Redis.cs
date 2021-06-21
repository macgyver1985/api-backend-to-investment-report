using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using InvestimentReport.Application.Interfaces.Adapters;
using StackExchange.Redis;
using InvestimentReport.Application.Helper;
using System.Text.RegularExpressions;

namespace InvestimentReport.Infrastructure.Cache
{

    public sealed class Redis : CacheHelper, ICache
    {

        private readonly ConnectionMultiplexer connection;

        public Redis(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.");

            if (string.IsNullOrEmpty(configuration["REDIS_URL"]))
                throw new ArgumentException("REDIS_URL is null.");

            string connectionString = string.Empty;

            if (new Regex(@"^(redis:\/\/)").IsMatch(configuration["REDIS_URL"]))
            {
                var redisUri = new Uri(configuration["REDIS_URL"]);
                var userInfo = redisUri.UserInfo.Split(':');

                connectionString = $"{redisUri.Host}:{redisUri.Port},password={userInfo[1]}";
            }
            else
                connectionString = configuration["REDIS_URL"];

            this.connection = ConnectionMultiplexer.Connect(connectionString);
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