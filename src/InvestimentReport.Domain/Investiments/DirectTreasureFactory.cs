using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Interfaces;
using InvestimentReport.Domain.Taxes;

namespace InvestimentReport.Domain.Investiments
{

    internal class DirectTreasureFactory : ITypeInvestimentFactory
    {

        public Investiment CreateInvestiment(InvestimentData data)
        {
            var item = new Investiment(
                ETypeInvestiment.DirectTreasure,
                data
            )
                .AddTaxes(new IR(10))
                .AddTaxes(new IOF());

            return item;
        }

    }

}