using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Guarantee;
using InvestmentReport.Domain.Interfaces;
using InvestmentReport.Domain.Taxes;

namespace InvestmentReport.Domain.Investments
{

    internal class FixedIncomeFactory : ITypeInvestmentFactory
    {

        public Investment CreateInvestment(InvestmentData data)
        {
            var item = new Investment(
                ETypeInvestment.FixedIncome,
                data
            )
                .AddTaxes(new IR(5))
                .AddTaxes(new IOF())
                .AddGuarantee(new FGV())
                .CalculateTaxes()
                .CalculateRedemptionValue();

            return item as Investment;
        }

    }

}