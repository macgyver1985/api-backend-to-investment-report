using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Interfaces;
using InvestmentReport.Domain.Taxes;

namespace InvestmentReport.Domain.Investments
{

    internal class DirectTreasureFactory : ITypeInvestmentFactory
    {

        public Investment CreateInvestment(InvestmentData data)
        {
            var item = new Investment(
                ETypeInvestment.DirectTreasure,
                data
            )
                .AddTaxes(new IR(10))
                .AddTaxes(new IOF())
                .CalculateTaxes()
                .CalculateRedemptionValue();

            return item as Investment;
        }

    }

}