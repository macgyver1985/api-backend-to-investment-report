using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InvestmentReport.Application.DTOs;
using InvestmentReport.Application.Helper;
using InvestmentReport.Application.Interfaces.Adapters;
using InvestmentReport.Application.Interfaces.Services;
using InvestmentReport.CrossCutting.Trace.Interfaces;
using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Investments;

namespace InvestmentReport.Application.Service
{

    public sealed class ObtainAllInvestmentsHandler : HandlerHelper, IObtainAllInvestmentsHandler
    {

        private readonly ILogger loggerAdapter;
        private readonly IGetInvestments getInvestmentsAdapter;

        public ObtainAllInvestmentsHandler(
            ILogger loggerAdapter,
            IGetInvestments getInvestmentsAdapter
        )
        {
            if (loggerAdapter == null)
                throw new ArgumentException("Logger adapter is null.");

            if (getInvestmentsAdapter == null)
                throw new ArgumentException("Get investments adapter is null.");

            this.loggerAdapter = loggerAdapter;
            this.getInvestmentsAdapter = getInvestmentsAdapter;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                this.loggerAdapter.Dispose();
                this.getInvestmentsAdapter.Dispose();
            }

            disposed = true;
        }

        private async Task<IList<T>> GetInvestment<T>(Guid processId)
        {
            try
            {
                var list = await this.getInvestmentsAdapter.GetOf<T>(processId);

                return list;
            }
            catch (Exception ex)
            {
                await this.loggerAdapter
                    .Error<ObtainAllInvestmentsHandler>(
                        processId,
                        $"Erro na integração para obter os investmento de {typeof(T).Name}.",
                        ex
                    );
            }

            return null;
        }

        public async Task<IList<Investment>> Execute(Guid processId)
        {
            if (processId == Guid.Empty)
                throw new ArgumentException("processId is empty.");

            List<Investment> result = new List<Investment>();

            (await this.GetInvestment<DirectTreasureDTO>(processId))?
                .ToList().ForEach(t =>
                    {
                        var item = InvestmentFactory.CreateInvestment(
                            ETypeInvestment.DirectTreasure,
                            new InvestmentData()
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

            (await this.GetInvestment<FixedIncomeDTO>(processId))?
                .ToList().ForEach(t =>
                    {
                        var item = InvestmentFactory.CreateInvestment(
                            ETypeInvestment.FixedIncome,
                            new InvestmentData()
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

            (await this.GetInvestment<FundsDTO>(processId))?
                .ToList().ForEach(t =>
                    {
                        var item = InvestmentFactory.CreateInvestment(
                            ETypeInvestment.Funds,
                            new InvestmentData()
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