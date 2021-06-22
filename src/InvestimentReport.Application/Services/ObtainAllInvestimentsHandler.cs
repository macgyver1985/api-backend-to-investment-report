using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestimentReport.Application.DTOs;
using InvestimentReport.Application.Helper;
using InvestimentReport.Application.Interfaces.Adapters;
using InvestimentReport.Application.Interfaces.Services;
using InvestimentReport.CrossCutting.Trace.Interfaces;
using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Investiments;

namespace InvestimentReport.Application.Service
{

    public sealed class ObtainAllInvestimentsHandler : HandlerHelper, IObtainAllInvestimentsHandler
    {

        private readonly ILogger loggerAdapter;
        private readonly IGetInvestiments getInvestimentsAdapter;

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
                this.loggerAdapter.Dispose();
                this.getInvestimentsAdapter.Dispose();
            }

            disposed = true;
        }

        private async Task<IList<T>> GetInvestiment<T>(Guid processId)
        {
            try
            {
                var list = await this.getInvestimentsAdapter.GetOf<T>(processId);

                return list;
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<ObtainAllInvestimentsHandler>(
                        processId,
                        $"Erro na integração para obter os investimento de {typeof(T).Name}.",
                        ex
                    );
            }

            return null;
        }

        public async Task<IList<Investiment>> Execute(Guid processId)
        {
            if (processId == Guid.Empty)
                throw new ArgumentException("processId is empty.");

            List<Investiment> result = new List<Investiment>();

            (await this.GetInvestiment<DirectTreasureDTO>(processId))?
                .ToList().ForEach(t =>
                    {
                        var item = InvestimentFactory.CreateInvestiment(
                            ETypeInvestiment.DirectTreasure,
                            new InvestimentData()
                            {
                                InvestedValue = t.InvestedValue,
                                CurrentValue = t.Amount,
                                DueDate = t.DueDate,
                                PurchaseDate = t.PurchaseDate,
                                Index = t.Index,
                                Name = t.Name
                            }
                        );

                        result.Add(item);
                    });

            (await this.GetInvestiment<FixedIncomeDTO>(processId))?
                .ToList().ForEach(t =>
                    {
                        var item = InvestimentFactory.CreateInvestiment(
                            ETypeInvestiment.FixedIncome,
                            new InvestimentData()
                            {
                                InvestedValue = t.InvestedCapital,
                                CurrentValue = t.CurrentCapital,
                                DueDate = t.DueDate,
                                PurchaseDate = t.OperationDate,
                                Index = t.Index,
                                Name = t.Name,
                                Quantity = t.Quantity,
                                UnitaryValue = t.UnitaryValue
                            }
                        );

                        result.Add(item);
                    });

            (await this.GetInvestiment<FundsDTO>(processId))?
                .ToList().ForEach(t =>
                    {
                        var item = InvestimentFactory.CreateInvestiment(
                            ETypeInvestiment.Funds,
                            new InvestimentData()
                            {
                                InvestedValue = t.InvestedCapital,
                                CurrentValue = t.CurrentValue,
                                DueDate = t.RedemptionDate,
                                Name = t.Name,
                                PurchaseDate = t.PurchaseDate,
                                Quantity = t.Quantity,
                                AdministrativeTax = t.TotalTaxes
                            }
                        );

                        result.Add(item);
                    });

            if (!result.Any())
                return null;

            return result;
        }

    }

}