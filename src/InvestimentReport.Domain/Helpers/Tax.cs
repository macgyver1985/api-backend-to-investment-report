using InvestimentReport.Domain.Interfaces;

namespace InvestimentReport.Domain.Helpers
{

    public abstract class Tax : ITax
    {

        public string Name { get; protected set; }

        public double CalculationBasis { get; protected set; }

        public bool IsCalculated { get; protected set; }

        public double Value { get; protected set; }

        public abstract ITax Calculate(IInvestiment investiment);

    }

}