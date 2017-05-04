namespace ZopaQuote.Entities
{
    public class MarketData
    {
        public int LineNumber { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public int AvailableAmount { get; set; }
    }
}