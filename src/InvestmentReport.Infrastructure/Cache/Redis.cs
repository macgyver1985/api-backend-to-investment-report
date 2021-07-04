using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;
using InvestmentReport.Application.Interfaces.Adapters;
using StackExchange.Redis;
using InvestmentReport.Application.Helper;
using System.Text.RegularExpressions;

namespace InvestmentReport.Infrastructure.Cache
{

    public sealed class Redis : AdapterHelper, ICache
    {

        private ConnectionMultiplexer connection;
        private readonly string connectionString;

        public Redis(IConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.");

            if (string.IsNullOrEmpty(configuration["REDIS_URL"]))
                throw new ArgumentException("REDIS_URL is null.");

            if (new Regex(@"^(redis:\/\/)").IsMatch(configuration["REDIS_URL"]))
            {
                var redisUri = new Uri(configuration["REDIS_URL"]);
                var userInfo = redisUri.UserInfo.Split(':');

                this.connectionString = $"{redisUri.Host}:{redisUri.Port},password={userInfo[1]}";
            }
            else
                this.connectionString = configuration["REDIS_URL"];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.connection?.Close();
                this.connection?.Dispose();
            }

            disposed = true;
        }

        private async Task Connection()
        {
            if (this.connection == null)
                this.connection = await ConnectionMultiplexer.ConnectAsync(connectionString);
        }

        public async Task<string> Obtain(string key)
        {
            await this.Connection();

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
            await this.Connection();

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
            await this.Connection();

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