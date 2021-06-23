using System;
using InvestmentReport.Domain.Helpers;
using InvestmentReport.Domain.Interfaces;

namespace InvestmentReport.Domain.Taxes
{

    public sealed class IR : Tax
    {

        public IR(double calculationBasis)
        {
            this.Name = "IR";
            this.CalculationBasis = calculationBasis;
        }

        public override ITax Calculate(IInvestment investment)
        {
            double profitability = investment.InvestedValue - investment.CurrentValue;
            double tempValue = 0D;

            if (profitability < 0)
            {
                profitability *= -1;

                tempValue = (profitability * this.CalculationBasis) / 100;
                tempValue = Math.Round(tempValue, 4);
            }

            this.Value = tempValue;
            this.IsCalculated = true;

            return this;
        }
    }

}