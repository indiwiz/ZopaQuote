namespace ZopaQuote
{
    public class AppConfiguration
    {
        public int TotalNumberOfPayments { get; set; }
        public NumberRange LoanAmountRange { get; set; } = new NumberRange();
        public class NumberRange
        {
            public int Minimum { get; set; }
            public int Maximum { get; set; }
        }
    }
}
