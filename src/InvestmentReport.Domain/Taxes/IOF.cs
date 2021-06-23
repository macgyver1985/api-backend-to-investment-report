using InvestmentReport.Domain.Helpers;
using InvestmentReport.Domain.Interfaces;

namespace InvestmentReport.Domain.Taxes
{

    public sealed class IOF : Tax
    {

        public IOF()
        {
            this.Name = "IOF";
            this.CalculationBasis = 0D;
        }

        public override ITax Calculate(IInvestment investment)
        {
            this.Value = 0D;
            this.IsCalculated = true;

            return this;
        }
    }

}