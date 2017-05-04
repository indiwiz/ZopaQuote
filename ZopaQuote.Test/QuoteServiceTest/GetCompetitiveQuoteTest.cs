using Xunit;
using ZopaQuote.Entities;

namespace ZopaQuote.Test.QuoteServiceTest
{
    public class GetCompetitiveQuoteTest
    {
        [Theory]
        [MemberData(nameof(QuotationDataSource.TestData), MemberType = typeof(QuotationDataSource))]
        public void Should(MarketData[] input, int loanAmount, MarketData expectedOutput)
        {
            var sut = Helper.GetServiceUnderTest(input);
            var output = sut.GetCompetitiveQuote(loanAmount);
            Assert.Equal(expectedOutput, output);
        }
    }
}
