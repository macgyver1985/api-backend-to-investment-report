using InvestimentReport.Domain.Investiments;

namespace InvestimentReport.Domain.Interfaces
{

    internal interface ITypeInvestimentFactory
    {

        Investiment CreateInvestiment(InvestimentData data);

    }

}