using InvestmentReport.Domain.Helpers;
using InvestmentReport.Domain.Interfaces;

namespace InvestmentReport.Domain.Taxes
{

    public sealed class Administrative : Tax
    {

        public Administrative(double calculationBasis)
        {
            this.Name = "Taxa de Administração";
            this.CalculationBasis = calculationBasis;
        }

        public override ITax Calculate(IInvestment investment)
        {
            this.Value = this.CalculationBasis;
            this.IsCalculated = true;

            return this;
        }
    }

}