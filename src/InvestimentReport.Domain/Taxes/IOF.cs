using InvestimentReport.Domain.Helpers;
using InvestimentReport.Domain.Investiments;

namespace InvestimentReport.Domain.Taxes
{

    public sealed class IOF : Tax
    {

        public IOF()
        {
            this.Name = "IOF";
            this.CalculationBasis = 0D;
        }

        public override void Calculate(Investiment investiment)
        {
            this.Value = 0D;
            this.IsCalculated = true;
        }
    }

}