using Xunit;
using ZopaQuote.Entities;

namespace ZopaQuote.Test.QuoteServiceTest
{
    public class GetCompetitiveQuoteTest
    {
        [Theory]
        [MemberData(nameof(QuotationDataSource.TestData), MemberType = typeof(QuotationDataSource))]
        public void Should_WorkInAllCases(MarketData[] input, int loanAmount, MarketData expectedOutput)
        {
            var sut = Helper.GetServiceUnderTest(input);
            var output = sut.GetCompetitiveQuote(loanAmount);
            if (expectedOutput != null)
            {
                Assert.Equal(expectedOutput.Rate, output.Rate);
            }
            else
            {
                Assert.Null(output);
            }
        }
    }
}
