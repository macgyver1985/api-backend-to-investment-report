namespace InvestimentReport.Domain.Interfaces
{

    public interface ITax
    {

        string Name { get; }

        double CalculationBasis { get; }

        bool IsCalculated { get; }

        double Value { get; }

        ITax Calculate(IInvestiment investiment);

    }

}