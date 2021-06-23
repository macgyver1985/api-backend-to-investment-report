using System;
using System.Collections.Generic;
using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Helpers;

namespace InvestmentReport.Domain.Interfaces
{

    public interface IInvestment
    {

        double InvestedValue { get; }

        double CurrentValue { get; }

        double UnitaryValue { get; }

        double RedemptionValue { get; }

        double LossValueOnRedemption { get; }

        double TotalTaxes { get; }

        DateTime DueDate { get; }

        DateTime PurchaseDate { get; }

        TimeSpan TotalTime { get; }

        string Index { get; }

        ETypeInvestment Type { get; }

        string Name { get; }

        double Quantity { get; }

        bool IsValid { get; }

        IReadOnlyList<ITax> Taxes { get; }

        IReadOnlyList<IGuarantee> GuaranteeList { get; }

        IInvestment CalculateRedemptionValue();

        IInvestment CalculateTaxes();

        IInvestment AddTaxes(ITax tax);

        IInvestment AddGuarantee(IGuarantee guarantee);

    }

}