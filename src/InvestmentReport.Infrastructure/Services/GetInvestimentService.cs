using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using InvestmentReport.Application.Helper;
using InvestmentReport.Application.Interfaces.Adapters;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace InvestmentReport.Infrastructure.Services
{

    public sealed class GetInvestmentService : AdapterHelper, IGetInvestments
    {

        private readonly IConfiguration configuration;
        private readonly ICache cacheAdapter;
        private readonly ILogger loggerAdapter;
        private readonly string cacheKey;
        private readonly string cacheTotalTime;

        public GetInvestmentService(
            IConfiguration configuration,
            ICache cacheAdapter,
            ILogger loggerAdapter
        )
        {
            if (configuration == null)
                throw new ArgumentException("Configuration is null.");

            if (cacheAdapter == null)
                throw new ArgumentException("Cache adapter is null.");

            if (loggerAdapter == null)
                throw new ArgumentException("Logger adapter is null.");

            this.configuration = configuration;
            this.cacheAdapter = cacheAdapter;
            this.loggerAdapter = loggerAdapter;

            this.cacheKey = configuration[$"{nameof(GetInvestmentService)}:cacheKey"];
            this.cacheTotalTime = configuration[$"{nameof(GetInvestmentService)}:cacheTotalTime"];
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.cacheAdapter.Dispose();
                this.loggerAdapter.Dispose();
            }

            disposed = true;
        }

        public async Task<IList<T>> GetOf<T>(Guid processId)
        {
            List<T> result = await this.GetCachedInvestments<T>(processId) ?? null;

            if (result != null && result.Any())
                return result;

            string url = $"{nameof(GetInvestmentService)}:{new Regex("(DTO)?$", RegexOptions.IgnoreCase).Replace(typeof(T).Name, string.Empty)}";
            IRestResponse response = null;

            try
            {
                RestClient restClient = new RestClient(this.configuration[url]);
                RestRequest restRequest = new RestRequest(Method.GET)
                {
                    RequestFormat = DataFormat.Json
                };

                restRequest.AddHeader("Accept", "application/json");
                restRequest.AddHeader("cache-control", "no-cache");

                response = restClient.Get(restRequest);

                var jObject = JObject.Parse(response.Content);

                result = ServiceResponseFactory.ConvertList<T>(jObject);
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<GetInvestmentService, object>(
                        processId,
                        $"Erro ao tentar obter a lista de investmentos {typeof(T).Name}.",
                        ex,
                        new
                        {
                            urlService = url,
                            response = response?.Content,
                            statusCode = response?.StatusCode
                        }
                    );
            }

            if (result.Any())
            {
                await this.ResgisterInvestmentsInCache(processId, result);

                return result;
            }

            return null;
        }

        private async Task<List<T>> GetCachedInvestments<T>(Guid processId)
        {
            try
            {
                string responseCache = await this.cacheAdapter.Obtain($"{this.cacheKey}{typeof(T).Name}");

                if (!string.IsNullOrWhiteSpace(responseCache))
                    return JsonConvert.DeserializeObject<List<T>>(responseCache);
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<GetInvestmentService>(
                        processId,
                        $"Erro ao tentar obter o cache {this.cacheKey}{typeof(T).Name}.",
                        ex
                    );
            }

            return null;
        }

        private async Task ResgisterInvestmentsInCache<T>(Guid processId, List<T> list)
        {
            string payload = string.Empty;

            try
            {
                TimeSpan expire = TimeSpan.Parse(this.cacheTotalTime) - DateTime.UtcNow.TimeOfDay;

                payload = JsonConvert.SerializeObject(list, Formatting.Indented);

                await this.cacheAdapter?.Register(
                    $"{this.cacheKey}{typeof(T).Name}",
                    payload,
                    expire
                );
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<GetInvestmentService, List<T>>(
                        processId,
                        $"Erro ao tentar registrar o cache {this.cacheKey}{typeof(T).Name}.",
                        ex,
                        list
                    );
            }
        }

    }

}