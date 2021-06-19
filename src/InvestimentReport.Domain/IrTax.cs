using System;
using InvestimentReport.Domain.Helper;

namespace InvestimentReport.Domain
{

    public sealed class IrTax : Tax
    {

        public IrTax(double calculationBasis)
        {
            this.Name = "IR";
            this.CalculationBasis = calculationBasis;
        }

        public override void Calculate(Investiment investiment)
        {
            double profitability = investiment.InvestedValue - investiment.CurrentValue;
            double tempValue = 0D;

            if (profitability > 0)
            {
                tempValue = (profitability * this.CalculationBasis) / 100;
                tempValue = Math.Round(tempValue, 4);
            }

            this.Value = tempValue;
            this.IsCalculated = true;
        }
    }

}