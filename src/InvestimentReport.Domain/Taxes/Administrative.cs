using InvestimentReport.Domain.Helpers;
using InvestimentReport.Domain.Interfaces;

namespace InvestimentReport.Domain.Taxes
{

    public sealed class Administrative : Tax
    {

        public Administrative(double calculationBasis)
        {
            this.Name = "Taxa de Administração";
            this.CalculationBasis = calculationBasis;
        }

        public override ITax Calculate(IInvestiment investiment)
        {
            this.Value = this.CalculationBasis;
            this.IsCalculated = true;

            return this;
        }
    }

}