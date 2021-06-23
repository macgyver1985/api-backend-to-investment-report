namespace InvestmentReport.Domain.Interfaces
{

    public interface ITax
    {

        string Name { get; }

        double CalculationBasis { get; }

        bool IsCalculated { get; }

        double Value { get; }

        ITax Calculate(IInvestment investment);

    }

}