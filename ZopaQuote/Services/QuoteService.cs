using System.Collections.Generic;
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

        public IEnumerable<Quote> GetCompetitiveQuote(int amount)
        {
            return _marketDataContext.MarketData
                .Where(x => x.AvailableAmount > amount)
                .OrderBy(x => x.Rate)
                .Select(d => new Quote(d.Name, amount, d.Rate, _totalNumberOfPayments));
        }


    }
}
