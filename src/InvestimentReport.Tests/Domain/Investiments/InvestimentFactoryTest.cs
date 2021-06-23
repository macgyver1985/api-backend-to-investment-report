using System;
using System.Collections.Generic;
using System.Linq;
using InvestimentReport.Domain.Enums;
using InvestimentReport.Domain.Guarantee;
using InvestimentReport.Domain.Interfaces;
using InvestimentReport.Domain.Investiments;
using Xunit;

namespace InvestimentReport.Tests.Domain.Investiments
{

    public class InvestimentFactoryTest
    {

        [Fact]
        public void CreateInvestimentDirectTreeasureSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.DirectTreasure);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.DirectTreasure,
                    new InvestimentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentDirectTreeasureSuccessTwoTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.DirectTreasure);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.DirectTreasure,
                    new InvestimentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentDirectTreeasureSuccessThreeTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.DirectTreasure);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.DirectTreasure,
                    new InvestimentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentDirectTreeasureSuccessFourTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.DirectTreasure);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.DirectTreasure,
                    new InvestimentData()
                    {
                        Name = "Tesouro Selic 2025",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "SELIC",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFixedIncomeSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.FixedIncome);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.FixedIncome,
                    new InvestimentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFixedIncomeTwoSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.FixedIncome);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.FixedIncome,
                    new InvestimentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFixedIncomeThreeSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.FixedIncome);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.FixedIncome,
                    new InvestimentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFixedIncomeFourSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.FixedIncome);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.FixedIncome,
                    new InvestimentData()
                    {
                        Name = "BANCO MAXIMA",
                        CurrentValue = 1100,
                        DueDate = DateTime.Parse(finalDate),
                        PurchaseDate = DateTime.Parse(initialDate),
                        Index = "97% do CDI",
                        InvestedValue = 1000,
                    }
                );

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFundsSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.Funds);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.Funds,
                    new InvestimentData()
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

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFundsTwoSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.Funds);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.Funds,
                    new InvestimentData()
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

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFundsThreeSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.Funds);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.Funds,
                    new InvestimentData()
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

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

        [Fact]
        public void CreateInvestimentFundsFourSuccessTest()
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

            Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
            investMock.Setup(x => x.Type).Returns(ETypeInvestiment.Funds);

            var investiment = InvestimentFactory
                .CreateInvestiment(
                    ETypeInvestiment.Funds,
                    new InvestimentData()
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

            Assert.Equal(investiment.Name, investMock.Object.Name);
            Assert.Equal(investiment.CurrentValue, investMock.Object.CurrentValue);
            Assert.Equal(investiment.DueDate, investMock.Object.DueDate);
            Assert.Equal(investiment.PurchaseDate, investMock.Object.PurchaseDate);
            Assert.Equal(investiment.Index, investMock.Object.Index);
            Assert.Equal(investiment.InvestedValue, investMock.Object.InvestedValue);
            Assert.Equal(investiment.Quantity, investMock.Object.Quantity);
            Assert.Equal(investiment.UnitaryValue, investMock.Object.UnitaryValue);
            Assert.Equal(investiment.IsValid, investMock.Object.IsValid);
            Assert.Equal(investiment.GuaranteeList.Count, investMock.Object.GuaranteeList.Count);
            Assert.Equal(investiment.LossValueOnRedemption, investMock.Object.LossValueOnRedemption);
            Assert.Equal(investiment.RedemptionValue, investMock.Object.RedemptionValue);
            Assert.Equal(investiment.Taxes.Count, investMock.Object.Taxes.Count);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IR").Value, irMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "IOF").Value, iofMock.Object.Value);
            Assert.Equal(investiment.Taxes.FirstOrDefault(t => t.Name == "Taxa de Administração").Value, administrativeMock.Object.Value);
            Assert.Equal(investiment.TotalTaxes, investMock.Object.TotalTaxes);
            Assert.Equal(investiment.TotalTime, investMock.Object.TotalTime);
            Assert.Equal(investiment.Type, investMock.Object.Type);
        }

    }

}