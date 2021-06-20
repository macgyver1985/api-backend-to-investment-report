using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Guarantee;
using InvestimentReport.Domain.Interfaces;
using InvestimentReport.Domain.Taxes;

namespace InvestimentReport.Domain.Investiments
{

    internal class FixedIncomeFactory : ITypeInvestimentFactory
    {

        public Investiment CreateInvestiment(InvestimentData data)
        {
            var item = new Investiment(
                ETypeInvestiment.FixedIncome,
                data
            )
                .AddTaxes(new IR(5))
                .AddTaxes(new IOF())
                .AddGuarantee(new FGV());

            return item;
        }

    }

}