using System;
using InvestmentReport.Domain.Interfaces;
using InvestmentReport.Domain.Taxes;
using Xunit;

namespace InvestmentReport.Tests.Domain.Taxes
{

    public class IRTest
    {

        private readonly Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

        public IRTest()
        {
            investMock.Setup(t => t.InvestedValue).Returns(1000);
            investMock.Setup(t => t.CurrentValue).Returns(1100);
        }

        [Fact]
        public void ConstructorSuccessTest()
        {
            var expect = 10D;
            var expect2 = false;
            var expect3 = "IR";
            var expect4 = 0D;
            var tax = new IR(expect);

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

        [Fact]
        public void CalculeSuccessTest()
        {
            var expect = 10D;
            var expect2 = true;
            var expect3 = "IR";
            var expect4 = 10D;
            var tax = new IR(expect)
                .Calculate(this.investMock.Object);

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

        [Fact]
        public void CalculeSuccessTwoTest()
        {
            investMock.Setup(t => t.InvestedValue).Returns(1100);
            investMock.Setup(t => t.CurrentValue).Returns(1000);

            var expect = 10D;
            var expect2 = true;
            var expect3 = "IR";
            var expect4 = 0D;
            var tax = new IR(expect)
                .Calculate(this.investMock.Object);

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

    }

}
