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

    /// <summary>
    /// Serviço de dominio que devolve a lista de investimento do cliente.
    /// </summary>
    public sealed class ObtainAllInvestmentsHandler : HandlerHelper, IObtainAllInvestmentsHandler
    {

        private readonly ILogger loggerAdapter;
        private readonly IGetInvestments getInvestmentsAdapter;

        /// <summary>
        /// Construtor padrão.
        /// </summary>
        /// <param name="loggerAdapter">Recebe a implementação concreta do ILogger.</param>
        /// <param name="getInvestmentsAdapter">Recebe a implementação concreta do IGetInvestments.</param>
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
                this.getInvestmentsAdapter.Dispose();
            }

            disposed = true;
        }

        /// <summary>
        /// Método auxiliar para chamar o IGetInvestments.
        /// </summary>
        /// <typeparam name="T">Tipos do DTO que será buscado.</typeparam>
        /// <param name="processId">Identificação do processamento.</param>
        /// <returns>Retorna uma lista de T.</returns>
        private async Task<IList<T>> GetInvestment<T>(Guid processId)
        {
            try
            {
                this.loggerAdapter
                    .Debug(
                        processId,
                        $"Iniciando integração com IGetInvestments",
                        typeof(T).Name
                    );

                var list = await this.getInvestmentsAdapter.GetOf<T>(processId);

                return list;
            }
            catch (Exception ex)
            {
                this.loggerAdapter
                    .Error(
                        processId,
                        $"Erro na integração para obter os investmento de {typeof(T).Name}.",
                        ex
                    );
            }

            return null;
        }

        /// <summary>
        /// Método que devolve a lista consolidade dos investimentos do cliente.
        /// </summary>
        /// <param name="processId">Identificação do processo.</param>
        /// <returns>Retorna lista de Investment.</returns>
        public async Task<IList<Investment>> Execute(Guid processId)
        {
            if (processId == Guid.Empty)
                throw new ArgumentException("processId is empty.");

            this.loggerAdapter
                .Debug(
                    processId,
                    $"Iniciando execução do serviço de dominio {nameof(ObtainAllInvestmentsHandler)}"
                );

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

            this.loggerAdapter
                .Debug(
                    processId,
                    $"Execução de ${nameof(ObtainAllInvestmentsHandler)} finalizada."
                );

            if (!result.Any())
                return null;

            return result;
        }

    }

}