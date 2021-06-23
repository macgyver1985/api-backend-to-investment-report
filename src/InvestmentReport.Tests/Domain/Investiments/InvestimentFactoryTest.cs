using System;
using System.Collections.Generic;
using System.Linq;
using InvestmentReport.Domain.Enums;
using InvestmentReport.Domain.Guarantee;
using InvestmentReport.Domain.Interfaces;
using InvestmentReport.Domain.Investments;
using Xunit;

namespace InvestmentReport.Tests.Domain.Investments
{

    public class InvestmentFactoryTest
    {

        [Fact]
        public void CreateInvestmentDirectTreeasureSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(10).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(10D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(10D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("Tesouro Selic 2025");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("SELIC");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(300D);
            investMock.Setup(x => x.RedemptionValue).Returns(790D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(10D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.DirectTreasure);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.DirectTreasure,
                    new InvestmentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentDirectTreeasureSuccessTwoTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-10).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(2).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(10D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(10D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("Tesouro Selic 2025");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("SELIC");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(60D);
            investMock.Setup(x => x.RedemptionValue).Returns(1030D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(10D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.DirectTreasure);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.DirectTreasure,
                    new InvestmentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentDirectTreeasureSuccessThreeTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-7).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(3).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(10D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(10D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("Tesouro Selic 2025");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("SELIC");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(150D);
            investMock.Setup(x => x.RedemptionValue).Returns(940D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(10D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.DirectTreasure);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.DirectTreasure,
                    new InvestmentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentDirectTreeasureSuccessFourTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-10).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(0).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(10D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(10D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("Tesouro Selic 2025");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("SELIC");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(0D);
            investMock.Setup(x => x.RedemptionValue).Returns(1090D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(10D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.DirectTreasure);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.DirectTreasure,
                    new InvestmentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFixedIncomeSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(10).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(5D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(5D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("BANCO MAXIMA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("97% do CDI");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>() { new FGV() });
            investMock.Setup(x => x.LossValueOnRedemption).Returns(300D);
            investMock.Setup(x => x.RedemptionValue).Returns(795D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(5D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.FixedIncome);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.FixedIncome,
                    new InvestmentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFixedIncomeTwoSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-8).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(2).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(5D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(5D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("BANCO MAXIMA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("97% do CDI");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>() { new FGV() });
            investMock.Setup(x => x.LossValueOnRedemption).Returns(60D);
            investMock.Setup(x => x.RedemptionValue).Returns(1035D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(5D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.FixedIncome);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.FixedIncome,
                    new InvestmentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFixedIncomeThreeSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-6).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(4).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(5D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(5D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("BANCO MAXIMA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("97% do CDI");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>() { new FGV() });
            investMock.Setup(x => x.LossValueOnRedemption).Returns(150D);
            investMock.Setup(x => x.RedemptionValue).Returns(945D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(5D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.FixedIncome);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.FixedIncome,
                    new InvestmentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFixedIncomeFourSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-10).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(0).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(5D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(5D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("BANCO MAXIMA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.Index).Returns("97% do CDI");
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(0D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>() { new FGV() });
            investMock.Setup(x => x.LossValueOnRedemption).Returns(0D);
            investMock.Setup(x => x.RedemptionValue).Returns(1095D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(5D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.FixedIncome);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.FixedIncome,
                    new InvestmentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFundsSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(10).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(15D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(15D);

            Moq.Mock<ITax> administrativeMock = new Moq.Mock<ITax>();

            administrativeMock.Setup(t => t.CalculationBasis).Returns(100D);
            administrativeMock.Setup(t => t.IsCalculated).Returns(true);
            administrativeMock.Setup(t => t.Name).Returns("Taxa de Administração");
            administrativeMock.Setup(t => t.Value).Returns(100D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("ALASKA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(1D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(300D);
            investMock.Setup(x => x.RedemptionValue).Returns(685D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object, administrativeMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(115D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.Funds);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.Funds,
                    new InvestmentData()
                    {
                        Name = "ALASKA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        InvestedValue = 1000,
                        AdministrativeTax = 100D,
                        Quantity = 1
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFundsTwoSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-8).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(2).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(15D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(15D);

            Moq.Mock<ITax> administrativeMock = new Moq.Mock<ITax>();

            administrativeMock.Setup(t => t.CalculationBasis).Returns(100D);
            administrativeMock.Setup(t => t.IsCalculated).Returns(true);
            administrativeMock.Setup(t => t.Name).Returns("Taxa de Administração");
            administrativeMock.Setup(t => t.Value).Returns(100D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("ALASKA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(1D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(60D);
            investMock.Setup(x => x.RedemptionValue).Returns(925D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object, administrativeMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(115D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.Funds);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.Funds,
                    new InvestmentData()
                    {
                        Name = "ALASKA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        InvestedValue = 1000,
                        AdministrativeTax = 100D,
                        Quantity = 1
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFundsThreeSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-6).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(4).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(15D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(15D);

            Moq.Mock<ITax> administrativeMock = new Moq.Mock<ITax>();

            administrativeMock.Setup(t => t.CalculationBasis).Returns(100D);
            administrativeMock.Setup(t => t.IsCalculated).Returns(true);
            administrativeMock.Setup(t => t.Name).Returns("Taxa de Administração");
            administrativeMock.Setup(t => t.Value).Returns(100D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("ALASKA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(1D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(150D);
            investMock.Setup(x => x.RedemptionValue).Returns(835D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object, administrativeMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(115D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.Funds);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.Funds,
                    new InvestmentData()
                    {
                        Name = "ALASKA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        InvestedValue = 1000,
                        AdministrativeTax = 100D,
                        Quantity = 1
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestmentFundsFourSuccessTest()
        {
            var initialDate = DateTime.UtcNow.AddMonths(-10).ToString("yyyy-MM-dd");
            var finalDate = DateTime.UtcNow.AddMonths(0).ToString("yyyy-MM-dd");

            Moq.Mock<ITax> iofMock = new Moq.Mock<ITax>();

            iofMock.Setup(t => t.CalculationBasis).Returns(0);
            iofMock.Setup(t => t.IsCalculated).Returns(true);
            iofMock.Setup(t => t.Name).Returns("IOF");
            iofMock.Setup(t => t.Value).Returns(0);

            Moq.Mock<ITax> irMock = new Moq.Mock<ITax>();

            irMock.Setup(t => t.CalculationBasis).Returns(15D);
            irMock.Setup(t => t.IsCalculated).Returns(true);
            irMock.Setup(t => t.Name).Returns("IR");
            irMock.Setup(t => t.Value).Returns(15D);

            Moq.Mock<ITax> administrativeMock = new Moq.Mock<ITax>();

            administrativeMock.Setup(t => t.CalculationBasis).Returns(100D);
            administrativeMock.Setup(t => t.IsCalculated).Returns(true);
            administrativeMock.Setup(t => t.Name).Returns("Taxa de Administração");
            administrativeMock.Setup(t => t.Value).Returns(100D);

            Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

            investMock.Setup(x => x.Name).Returns("ALASKA");
            investMock.Setup(x => x.CurrentValue).Returns(1100);
            investMock.Setup(x => x.DueDate).Returns(DateTime.Parse(finalDate));
            investMock.Setup(x => x.PurchaseDate).Returns(DateTime.Parse(initialDate));
            investMock.Setup(x => x.InvestedValue).Returns(1000);
            investMock.Setup(x => x.Quantity).Returns(1D);
            investMock.Setup(x => x.UnitaryValue).Returns(0D);
            investMock.Setup(x => x.IsValid).Returns(true);
            investMock.Setup(x => x.GuaranteeList).Returns(new List<IGuarantee>());
            investMock.Setup(x => x.LossValueOnRedemption).Returns(0D);
            investMock.Setup(x => x.RedemptionValue).Returns(985D);
            investMock.Setup(x => x.Taxes).Returns(new List<ITax>() { iofMock.Object, irMock.Object, administrativeMock.Object });
            investMock.Setup(x => x.TotalTaxes).Returns(115D);
            investMock.Setup(x => x.TotalTime).Returns(DateTime.Parse(finalDate).Subtract(DateTime.Parse(initialDate)));
            investMock.Setup(x => x.Type).Returns(ETypeInvestment.Funds);

            var investment = InvestmentFactory
                .CreateInvestment(
                    ETypeInvestment.Funds,
                    new InvestmentData()
                    {
                        Name = "ALASKA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        InvestedValue = 1000,
                        AdministrativeTax = 100D,
                        Quantity = 1
                    }
                );

            Assert.Equal(investment.Name, investMock.Object.Name);
            Assert.Equal(investment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investment.Index, investMock.Object.Index);
            Assert.Equal(investment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investment.Type, investMock.Object.Type);
        }

    }

}