using System.Linq;
using Moq;
using ZopaQuote.DataAccess;
using ZopaQuote.Entities;
using ZopaQuote.Services;

namespace ZopaQuote.Test.QuoteServiceTest
{
    public static class Helper
    {
        public static QuoteService GetServiceUnderTest(MarketData[] data)
        {
            var mock = new Mock<IMarketDataContext>();
            mock.SetupProperty(c => c.MarketData, data.ToList());
            return new QuoteService(mock.Object, new AppConfiguration());
        }
    }
}