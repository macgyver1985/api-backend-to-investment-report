using InvestmentReport.Domain.Guarantee;
using Xunit;

namespace InvestmentReport.Tests.Domain.Guarantee
{

    public class FGVTest
    {

        [Fact]
        public void ConstructorSuccessTest()
        {
            var expect = "Fundo Garantidor de Crédito";
            var expect2 = "FGV";
            var expect3 = 250000D;
            var guarantee = new FGV();

            Assert.Equal(guarantee.Name, expect);
            Assert.Equal(guarantee.Initials, expect2);
            Assert.Equal(guarantee.Value, expect3);
        }

    }

}