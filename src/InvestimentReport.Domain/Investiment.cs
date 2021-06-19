using System;
using System.Collections.Generic;
using System.Linq;
using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Helper;

namespace InvestimentReport.Domain
{

    public class Investiment
    {

        private readonly List<Tax> taxes = null;
        private readonly List<Guarantee> guaranteeList = null;
        private bool isCalculatedTaxes = false;
        private bool isCalculatedRedemptionValue = false;

        public Investiment()
        {
            this.taxes = new List<Tax>();
            this.guaranteeList = new List<Guarantee>();
        }

        public double InvestedValue { get; private set; }

        public int CurrentValue { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime PurchaseDate { get; private set; }

        public string Index { get; private set; }

        public ETypeInvestiment Type { get; }

        public string Name { get; private set; }

        public double Quantity { get; private set; }

        public double UnitaryValue { get; private set; }

        public bool IsValid
        {
            get
            {
                return this.isCalculatedTaxes && this.isCalculatedRedemptionValue;
            }
        }

        public IReadOnlyList<Tax> Taxes
        {
            get
            {
                return this.taxes;
            }
        }

        public IReadOnlyList<Guarantee> GuaranteeList
        {
            get
            {
                return this.guaranteeList;
            }
        }

        public Investiment CalculateRedemptionValue()
        {

            this.isCalculatedRedemptionValue = true;

            return this;
        }

        public Investiment CalculateTaxes()
        {
            this.taxes.ForEach(t =>
            {
                t.Calculate(this);
            });

            this.isCalculatedTaxes = true;

            return this;
        }

        public Investiment AddTaxes(Tax tax)
        {
            if (this.taxes.FirstOrDefault(t => t.Name == tax.Name) != null)
                return this;

            this.taxes.Add(tax);

            this.isCalculatedTaxes = false;

            return this;
        }

        public Investiment AddGuarantee(Guarantee guarantee)
        {
            if (this.guaranteeList.FirstOrDefault(t => t.Name == guarantee.Name) != null)
                return this;

            this.guaranteeList.Add(guarantee);

            return this;
        }

    }

}