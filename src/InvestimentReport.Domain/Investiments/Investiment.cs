using System;
using System.Collections.Generic;
using System.Linq;
using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Helpers;
using InvestimentReport.Domain.Interfaces;

namespace InvestimentReport.Domain.Investiments
{

    public class Investiment
    {

        #region Variables

        private readonly List<Tax> taxes = null;
        private readonly List<IGuarantee> guaranteeList = null;
        private bool isCalculatedTaxes = false;
        private bool isCalculatedRedemptionValue = false;
        private readonly TimeSpan halfOfTheTime;
        private readonly TimeSpan missingThreeMonths;

        #endregion

        #region Propertires

        public double InvestedValue { get; private set; }

        public double CurrentValue { get; private set; }

        public double UnitaryValue { get; private set; }

        public double RedemptionValue { get; private set; }

        public double LossValueOnRedemption { get; private set; }

        public double TotalTaxes { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime PurchaseDate { get; private set; }

        public TimeSpan TotalTime { get; private set; }

        public string Index { get; private set; }

        public ETypeInvestiment Type { get; private set; }

        public string Name { get; private set; }

        public double Quantity { get; private set; }

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

        public IReadOnlyList<IGuarantee> GuaranteeList
        {
            get
            {
                return this.guaranteeList;
            }
        }

        #endregion

        #region Constructors

        internal Investiment(ETypeInvestiment typeInvestiment, InvestimentData data)
        {
            this.taxes = new List<Tax>();
            this.guaranteeList = new List<IGuarantee>();

            this.Type = typeInvestiment;
            this.InvestedValue = data.InvestedValue;
            this.CurrentValue = data.CurrentValue;
            this.UnitaryValue = data.UnitaryValue;
            this.DueDate = data.DueDate;
            this.PurchaseDate = data.PurchaseDate;
            this.Index = data.Index;
            this.Name = data.Name;
            this.Quantity = data.Quantity;

            this.TotalTime = this.DueDate.Subtract(this.PurchaseDate);
            this.halfOfTheTime = this.TotalTime / 2;
            this.missingThreeMonths = this.DueDate.AddMonths(-3).Subtract(this.PurchaseDate);
        }

        #endregion

        #region Methods

        public Investiment CalculateRedemptionValue()
        {
            if (!this.isCalculatedTaxes)
                this.CalculateTaxes();

            TimeSpan timeLeft = this.DueDate.Subtract(DateTime.UtcNow);
            double lossPercentage = 30D;

            if (timeLeft > this.missingThreeMonths && timeLeft <= this.halfOfTheTime)
                lossPercentage = 15D;
            else if (timeLeft > TimeSpan.MinValue && timeLeft <= this.missingThreeMonths)
                lossPercentage = 6D;
            else if (timeLeft <= TimeSpan.MinValue)
                lossPercentage = 0D;

            double lossValue = (this.InvestedValue * lossPercentage) / 100;
            lossValue = Math.Round(lossValue, 3);

            this.LossValueOnRedemption = lossValue;
            this.RedemptionValue = this.CurrentValue - this.LossValueOnRedemption - this.TotalTaxes;
            this.isCalculatedRedemptionValue = true;

            return this;
        }

        public Investiment CalculateTaxes()
        {
            this.taxes.ForEach(t => t.Calculate(this));

            this.TotalTaxes = this.taxes.Sum(t => t.Value);
            this.isCalculatedTaxes = true;

            return this;
        }

        public Investiment AddTaxes(Tax tax)
        {
            if (this.taxes.FirstOrDefault(t => t.Name == tax.Name) != null)
                return this;

            this.taxes.Add(tax);

            this.TotalTaxes = 0D;
            this.LossValueOnRedemption = 0D;
            this.RedemptionValue = 0D;
            this.isCalculatedRedemptionValue = false;
            this.isCalculatedTaxes = false;

            return this;
        }

        public Investiment AddGuarantee(IGuarantee guarantee)
        {
            if (this.guaranteeList.FirstOrDefault(t => t.Name == guarantee.Name) != null)
                return this;

            this.guaranteeList.Add(guarantee);

            return this;
        }

        #endregion

    }

}