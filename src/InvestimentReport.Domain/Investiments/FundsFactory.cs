using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Interfaces;
using InvestimentReport.Domain.Taxes;

namespace InvestimentReport.Domain.Investiments
{

    internal class FundsFactory : ITypeInvestimentFactory
    {

        public Investiment CreateInvestiment(InvestimentData data)
        {
            var item = new Investiment(
                ETypeInvestiment.FixedIncome,
                data
            )
                .AddTaxes(new IR(15))
                .AddTaxes(new IOF())
                .AddTaxes(new Administrative(data.AdministrativeTax))
                .CalculateTaxes()
                .CalculateRedemptionValue();

            return item;
        }

    }

}