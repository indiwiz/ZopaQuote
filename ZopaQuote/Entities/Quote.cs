using System;

namespace ZopaQuote.Entities
{
    public class Quote
    {
        public double Rate { get; }

        public double MonthlyRepayment { get; }

        public double TotalRepayment { get; set; }

        public Quote(int principalAmount, double rate, int numberOfPayments)
        {
            Rate = rate;

            var effectiveRate = rate / 12;

            MonthlyRepayment = Math.Round(principalAmount * (effectiveRate / (1 - Math.Pow(1 + effectiveRate, -numberOfPayments))), 2);

            TotalRepayment = MonthlyRepayment * 36;
        }
    }
}
