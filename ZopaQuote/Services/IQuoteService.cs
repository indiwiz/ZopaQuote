using ZopaQuote.Entities;

namespace ZopaQuote.Services
{
    public interface IQuoteService
    {
        Quote GetCompetitiveQuote(int amount);
    }
}