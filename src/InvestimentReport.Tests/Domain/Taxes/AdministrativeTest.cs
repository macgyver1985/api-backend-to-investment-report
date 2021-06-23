using System;
using InvestimentReport.Domain.Interfaces;
using InvestimentReport.Domain.Taxes;
using Xunit;

namespace InvestimentReport.Tests.Domain.Taxes
{

    public class AdministrativeTest
    {

        private readonly Moq.Mock<IInvestiment> investMock = new Moq.Mock<IInvestiment>();

        [Fact]
        public void ConstructorSuccessTest()
        {
            var expect = 54D;
            var expect2 = false;
            var expect3 = "Taxa de Administração";
            var expect4 = 0D;
            var tax = new Administrative(expect);

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

        [Fact]
        public void CalculeSuccessTest()
        {
            var expect = 54D;
            var expect2 = true;
            var expect3 = "Taxa de Administração";
            var expect4 = 54D;
            var tax = new Administrative(54)
                .Calculate(this.investMock.Object);

            Assert.Equal(tax.CalculationBasis, expect);
            Assert.Equal(tax.IsCalculated, expect2);
            Assert.Equal(tax.Name, expect3);
            Assert.Equal(tax.Value, expect4);
        }

    }

}
