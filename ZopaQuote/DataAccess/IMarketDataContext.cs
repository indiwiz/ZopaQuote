using System.Collections.Generic;
using ZopaQuote.Entities;

namespace ZopaQuote.DataAccess
{
    public interface IMarketDataContext
    {
        List<MarketData> MarketData { get; set; }

        void Initialize(string fileName);
    }
}
