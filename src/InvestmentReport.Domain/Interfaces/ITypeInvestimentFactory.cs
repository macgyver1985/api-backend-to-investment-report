using InvestmentReport.Domain.Investments;

namespace InvestmentReport.Domain.Interfaces
{

    internal interface ITypeInvestmentFactory
    {

        Investment CreateInvestment(InvestmentData data);

    }

}