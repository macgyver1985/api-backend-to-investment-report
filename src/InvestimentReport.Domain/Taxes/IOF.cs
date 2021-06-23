using InvestimentReport.Domain.Helpers;
using InvestimentReport.Domain.Interfaces;

namespace InvestimentReport.Domain.Taxes
{

    public sealed class IOF : Tax
    {

        public IOF()
        {
            this.Name = "IOF";
            this.CalculationBasis = 0D;
        }

        public override ITax Calculate(IInvestiment investiment)
        {
            this.Value = 0D;
            this.IsCalculated = true;

            return this;
        }
    }

}