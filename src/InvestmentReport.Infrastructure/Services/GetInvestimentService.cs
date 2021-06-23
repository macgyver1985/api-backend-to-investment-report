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

    /// <summary>
    /// Classe que busca os investimentos do cliente de serviços externos.
    /// </summary>
    public sealed class GetInvestmentService : AdapterHelper, IGetInvestments
    {

        private readonly IConfiguration configuration;
        private readonly ICache cacheAdapter;
        private readonly ILogger loggerAdapter;
        private readonly string cacheKey;
        private readonly string cacheTotalTime;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="configuration">Intância de IConfiguration.</param>
        /// <param name="cacheAdapter">Instância da implementação concreta de ICache.</param>
        /// <param name="loggerAdapter">Instância da implementação concreta de ILogger.</param>
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

        /// <summary>
        /// Método que faz a requisição ao serviço externo para obter os dados.
        /// </summary>
        /// <typeparam name="T">Tipo do DTO que será obtido.</typeparam>
        /// <param name="processId">Identificação do processo.</param>
        /// <returns>Retorna lista de T.</returns>
        public async Task<IList<T>> GetOf<T>(Guid processId)
        {
            List<T> result = await this.GetCachedInvestments<T>(processId) ?? null;

            if (result != null && result.Any())
                return result;

            string url = $"{nameof(GetInvestmentService)}:{new Regex("(DTO)?$", RegexOptions.IgnoreCase).Replace(typeof(T).Name, string.Empty)}";
            IRestResponse response = null;

            try
            {
                await this.loggerAdapter
                    .Debug<GetInvestmentService, object>(
                        processId,
                        $"Executando integração",
                        new
                        {
                            urlService = this.configuration[url],
                            DTO = typeof(T).Name
                        }
                    );

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
                            urlService = this.configuration[url],
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

        /// <summary>
        /// Método auxiliar que obtem os dados do cache pelo ICache.
        /// </summary>
        /// <typeparam name="T">DTO que será obtido.</typeparam>
        /// <param name="processId">Identificação do processamento.</param>
        /// <returns>Retorna a lista de T que está cacheada.</returns>
        private async Task<List<T>> GetCachedInvestments<T>(Guid processId)
        {
            try
            {
                string responseCache = await this.cacheAdapter.Obtain($"{this.cacheKey}{typeof(T).Name}");

                if (!string.IsNullOrWhiteSpace(responseCache))
                {
                    await this.loggerAdapter
                        .Debug<GetInvestmentService, object>(
                            processId,
                            $"Lista de dados obtida do cache",
                            typeof(T).Name
                        );

                    return JsonConvert.DeserializeObject<List<T>>(responseCache);
                }
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

        /// <summary>
        /// Método auxiliar que faz registra o cache pelo ICache.
        /// </summary>
        /// <typeparam name="T">DTO que será cacheado.</typeparam>
        /// <param name="processId">Identificação do processamento.</param>
        /// <param name="list">Lista de dados que será cacheado.</param>
        /// <returns>Retorno void.</returns>
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

                await this.loggerAdapter
                    .Debug<GetInvestmentService, object>(
                        processId,
                        $"Lista de dados registrada no cache",
                        typeof(T).Name
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