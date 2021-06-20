using System;

namespace InvestimentReport.Domain.Investiments
{

    public class InvestimentData
    {

        public double InvestedValue { get; set; }

        public double CurrentValue { get; set; }

        public double UnitaryValue { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime PurchaseDate { get; set; }

        public string Index { get; set; }

        public string Name { get; set; }

        public double Quantity { get; set; }

        public double AdministrativeTax { get; set; }

    }

}