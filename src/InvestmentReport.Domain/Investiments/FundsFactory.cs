using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Interfaces;
using InvestmentReport.Domain.Taxes;

namespace InvestmentReport.Domain.Investments
{

    internal class FundsFactory : ITypeInvestmentFactory
    {

        public Investment CreateInvestment(InvestmentData data)
        {
            var item = new Investment(
                ETypeInvestment.Funds,
                data
            )
                .AddTaxes(new IR(15))
                .AddTaxes(new IOF())
                .AddTaxes(new Administrative(data.AdministrativeTax))
                .CalculateTaxes()
                .CalculateRedemptionValue();

            return item as Investment;
        }

    }

}