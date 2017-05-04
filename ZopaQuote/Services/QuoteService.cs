using System.Linq;
using ZopaQuote.DataAccess;
using ZopaQuote.Entities;

namespace ZopaQuote.Services
{
    public class QuoteService
    {
        private readonly IMarketDataContext _marketDataContext;

        public QuoteService(IMarketDataContext marketDataContext)
        {
            _marketDataContext = marketDataContext;
        }

        public MarketData GetCompetitiveQuote(int amount)
        {
            return _marketDataContext.MarketData
                .Where(x => x.AvailableAmount > amount)
                .OrderBy(x => x.Rate)
                .FirstOrDefault();            
        }
    }
}
