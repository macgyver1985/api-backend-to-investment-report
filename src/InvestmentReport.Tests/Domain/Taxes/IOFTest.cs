using System;
using InvestmentReport.Domain.Interfaces;
using InvestmentReport.Domain.Taxes;
using Xunit;

namespace InvestmentReport.Tests.Domain.Taxes
{

    public class IOFTest
    {

        private readonly Moq.Mock<IInvestment> investMock = new Moq.Mock<IInvestment>();

        [Fact]
        public void ConstructorSuccessTest()
        {
            var expect = 0D;
            var expect2 = false;
            var expect3 = "IOF";
            var expect4 = 0D;
            var tax = new IOF();

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

        [Fact]
        public void CalculeSuccessTest()
        {
            var expect = 0D;
            var expect2 = true;
            var expect3 = "IOF";
            var expect4 = 0D;
            var tax = new IOF()
                .Calculate(this.investMock.Object);

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

    }

}
