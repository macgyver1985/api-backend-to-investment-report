using System;
using InvestimentReport.Domain.Interfaces;
using InvestimentReport.Domain.Taxes;
using Xunit;

namespace InvestimentReport.Tests.Domain.Taxes
{

    public class IOFTest
    {

        private readonly Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

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
