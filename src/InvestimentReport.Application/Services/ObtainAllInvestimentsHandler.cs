using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.DTOs;
using InvestimentReport.Application.Helper;
using InvestimentReport.Application.Interfaces.Adapters;
using InvestimentReport.Application.Interfaces.Services;
using InvestimentReport.Domain;

namespace InvestimentReport.Application.Service
{

    public sealed class ObtainAllInvestimentsHandler : Handler, IObtainAllInvestimentsHandler
    {

        private readonly ILogger loggerAdapter = null;
        private readonly IGetInvestiments getInvestimentsAdapter = null;

        public ObtainAllInvestimentsHandler(
            ILogger loggerAdapter,
            IGetInvestiments getInvestimentsAdapter
        )
        {
            if (loggerAdapter == null)
                throw new ArgumentException("Logger adapter is null.");

            if (getInvestimentsAdapter == null)
                throw new ArgumentException("Get investments adapter is null.");

            this.loggerAdapter = loggerAdapter;
            this.getInvestimentsAdapter = getInvestimentsAdapter;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.loggerAdapter?.Dispose();
                this.getInvestimentsAdapter?.Dispose();
            }

            disposed = true;
        }

        public async Task<IList<Investiment>> Execute(Guid processId)
        {
            if (processId == Guid.Empty)
                throw new ArgumentException("processId is empty.");

            List<Investiment> result = new List<Investiment>();

            await Task.WhenAll(
                Task.Factory.StartNew(async () =>
                {
                    var list = await this.getInvestimentsAdapter.GetOf<DirectTreasureDTO>();

                    list?.ToList().ForEach(t =>
                        {
                            var item = new Investiment();

                            result.Add(item);
                        });
                }),
                Task.Factory.StartNew(async () =>
                {
                    var list = await this.getInvestimentsAdapter.GetOf<FixedIncomeDTO>();

                    list?.ToList().ForEach(t =>
                        {
                            var item = new Investiment();

                            result.Add(item);
                        });
                }),
                Task.Factory.StartNew(async () =>
                {
                    var list = await this.getInvestimentsAdapter.GetOf<FundsDTO>();

                    list?.ToList().ForEach(t =>
                        {
                            var item = new Investiment();

                            result.Add(item);
                        });
                })
            );

            return result;
        }

    }

}