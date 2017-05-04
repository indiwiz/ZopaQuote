using System.Linq;
using ZopaQuote.DataAccess;
using ZopaQuote.Entities;

namespace ZopaQuote.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly IMarketDataContext _marketDataContext;
        private readonly int _totalNumberOfPayments;

        public QuoteService(IMarketDataContext marketDataContext, AppConfiguration appConfiguration)
        {
            _marketDataContext = marketDataContext;
            _totalNumberOfPayments = appConfiguration.TotalNumberOfPayments;
        }

        public Quote GetCompetitiveQuote(int amount)
        {
            var marketData = _marketDataContext.MarketData
                .Where(x => x.AvailableAmount > amount)
                .OrderBy(x => x.Rate)
                .FirstOrDefault();
            return marketData == null ? null : new Quote(amount, marketData.Rate, _totalNumberOfPayments);
        }


    }
}
