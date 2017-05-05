using System.Collections.Generic;
using ZopaQuote.Entities;

namespace ZopaQuote.Services
{
    public interface IQuoteService
    {
        IEnumerable<Quote> GetCompetitiveQuote(int amount);
    }
}