using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.DTOs;
using InvestimentReport.Application.Helper;
using InvestimentReport.Application.Interfaces.Adapters;
using InvestimentReport.Application.Interfaces.Services;
using InvestimentReport.CrossCutting.Interfaces;
using InvestimentReport.Domain;
using Newtonsoft.Json;

namespace InvestimentReport.Application.Service
{

    public sealed class ObtainAllInvestimentsHandler : Handler, IObtainAllInvestimentsHandler
    {

        private readonly ICache cacheAdapter = null;
        private readonly ILogger loggerAdapter = null;
        private readonly IGetInvestiments getInvestimentsAdapter = null;
        private const string CACHE_KEY = "full-list-of-investments";
        private const string CACHE_EXPIRE = "0.24:00:00";

        public ObtainAllInvestimentsHandler(
            ICache cacheAdapter,
            ILogger loggerAdapter,
            IGetInvestiments getInvestimentsAdapter
        )
        {
            if (cacheAdapter == null)
                throw new ArgumentException("Cache adapter is null.");

            if (loggerAdapter == null)
                throw new ArgumentException("Logger adapter is null.");

            if (getInvestimentsAdapter == null)
                throw new ArgumentException("Get investments adapter is null.");

            this.cacheAdapter = cacheAdapter;
            this.loggerAdapter = loggerAdapter;
            this.getInvestimentsAdapter = getInvestimentsAdapter;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.cacheAdapter.Dispose();
                this.loggerAdapter.Dispose();
                this.getInvestimentsAdapter.Dispose();
            }

            disposed = true;
        }

        public async Task<IList<Investiment>> Execute(Guid processId)
        {
            if (processId == Guid.Empty)
                throw new ArgumentException("processId is empty.");

            List<Investiment> result = await this.GetCachedInvestments(processId) ?? new List<Investiment>();

            if (result.Any())
                return result;

            bool hasError = false;

            await Task.WhenAll(
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var list = await this.getInvestimentsAdapter.GetOf<DirectTreasureDTO>();

                        list?.ToList().ForEach(t =>
                            {
                                var item = new Investiment();

                                result.Add(item);
                            });
                    }
                    catch (Exception ex)
                    {
                        hasError = true;

                        await this.loggerAdapter
                            .Error<ObtainAllInvestimentsHandler>(
                                processId,
                                $"Erro na integração para obter os investimento de {nameof(DirectTreasureDTO)}.",
                                ex
                            );
                    }
                }),
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var list = await this.getInvestimentsAdapter.GetOf<FixedIncomeDTO>();

                        list?.ToList().ForEach(t =>
                            {
                                var item = new Investiment();

                                result.Add(item);
                            });
                    }
                    catch (Exception ex)
                    {
                        hasError = true;

                        await this.loggerAdapter
                            .Error<ObtainAllInvestimentsHandler>(
                                processId,
                                $"Erro na integração para obter os investimento de {nameof(FixedIncomeDTO)}.",
                                ex
                            );
                    }
                }),
                Task.Factory.StartNew(async () =>
                {
                    try
                    {
                        var list = await this.getInvestimentsAdapter.GetOf<FundsDTO>();

                        list?.ToList().ForEach(t =>
                            {
                                var item = new Investiment();

                                result.Add(item);
                            });
                    }
                    catch (Exception ex)
                    {
                        hasError = true;

                        await this.loggerAdapter
                            .Error<ObtainAllInvestimentsHandler>(
                                processId,
                                $"Erro na integração para obter os investimento de {nameof(FundsDTO)}.",
                                ex
                            );
                    }
                })
            );

            if (!hasError && result.Any())
                await this.ResgisterInvestimentsInCache(processId, result);

            return result;
        }

        private async Task<List<Investiment>> GetCachedInvestments(Guid processId)
        {
            try
            {
                string responseCache = await this.cacheAdapter.Obtain(CACHE_KEY);

                if (!string.IsNullOrWhiteSpace(responseCache))
                    return JsonConvert.DeserializeObject<List<Investiment>>(responseCache);
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<ObtainAllInvestimentsHandler>(
                        processId,
                        $"Erro ao tentar obter o cache {CACHE_KEY}.",
                        ex
                    );
            }

            return null;
        }

        private async Task ResgisterInvestimentsInCache(Guid processId, List<Investiment> list)
        {
            string payload = string.Empty;

            try
            {
                TimeSpan expire = TimeSpan.Parse(CACHE_EXPIRE) - DateTime.UtcNow.TimeOfDay;

                payload = JsonConvert.SerializeObject(list, Formatting.Indented);

                await this.cacheAdapter?.Register(
                    CACHE_KEY,
                    payload,
                    expire
                );
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<ObtainAllInvestimentsHandler>(
                        processId,
                        $"Erro ao tentar registrar o cache {CACHE_KEY}.",
                        ex,
                        payload
                    );
            }
        }

    }

}