using System;
using System.Collections.Generic;
using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Helpers;

namespace InvestimentReport.Domain.Interfaces
{

    public interface IInvestiment
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

        ETypeInvestiment Type { get; }

        string Name { get; }

        double Quantity { get; }

        bool IsValid { get; }

        IReadOnlyList<ITax> Taxes { get; }

        IReadOnlyList<IGuarantee> GuaranteeList { get; }

        IInvestiment CalculateRedemptionValue();

        IInvestiment CalculateTaxes();

        IInvestiment AddTaxes(ITax tax);

        IInvestiment AddGuarantee(IGuarantee guarantee);

    }

}