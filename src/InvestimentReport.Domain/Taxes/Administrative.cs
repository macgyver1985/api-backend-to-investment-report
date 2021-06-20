using InvestimentReport.Domain.Helpers;
using InvestimentReport.Domain.Investiments;

namespace InvestimentReport.Domain.Taxes
{

    public sealed class Administrative : Tax
    {

        public Administrative(double calculationBasis)
        {
            this.Name = "Taxa de Administração";
            this.CalculationBasis = calculationBasis;
        }

        public override void Calculate(Investiment investiment)
        {
            this.Value = this.CalculationBasis;
            this.IsCalculated = true;
        }
    }

}