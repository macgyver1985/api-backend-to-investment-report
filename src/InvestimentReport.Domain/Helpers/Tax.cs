using InvestimentReport.Domain.Investiments;

namespace InvestimentReport.Domain.Helpers
{

    public abstract class Tax
    {

        public string Name { get; protected set; }

        public double CalculationBasis { get; protected set; }

        public bool IsCalculated { get; protected set; }

        public double Value { get; protected set; }

        public abstract void Calculate(Investiment investiment);

    }

}