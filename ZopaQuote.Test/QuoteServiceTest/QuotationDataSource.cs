using System;
using System.Collections.Generic;
using ZopaQuote.Entities;

namespace ZopaQuote.Test.QuoteServiceTest
{
    public static class QuotationDataSource
    {
        private static readonly MarketData ValidData1 = new MarketData() {LineNumber = 1, Name = "One", Rate = 0.03m, AvailableAmount = 2000};
        private static readonly MarketData ValidData2 = new MarketData() { LineNumber = 2, Name = "Two", Rate = 0.025m, AvailableAmount = 4000 };
        private static readonly MarketData ValidData3 = new MarketData() { LineNumber = 3, Name = "Three", Rate = 0.017m, AvailableAmount = 6000 };
        private static readonly MarketData ValidData4 = new MarketData() { LineNumber = 4, Name = "Four", Rate = 0.033m, AvailableAmount = 10000 };
        private static readonly MarketData ValidData5 = new MarketData() { LineNumber = 5, Name = "Five", Rate = 0.024m, AvailableAmount = 2200 };
        private static readonly MarketData ValidData6 = new MarketData() { LineNumber = 6, Name = "Six", Rate = 0.014m, AvailableAmount = 20000 };
        private static readonly MarketData ValidData7 = new MarketData() { LineNumber = 7, Name = "Seven", Rate = 0.03m, AvailableAmount = 1000 };
        private static readonly MarketData ValidData8 = new MarketData() { LineNumber = 8, Name = "Eight", Rate = 0.025m, AvailableAmount = 4200 };
        private static readonly MarketData ValidData9 = new MarketData() { LineNumber = 9, Name = "Nine", Rate = 0.013m, AvailableAmount = 18000 };
        


        public static List<Object[]> TestData => new List<object[]>
        {
            new object[] {new[]{ValidData1, ValidData2, ValidData3, ValidData4 }, 5000, ValidData3},
            new object[] {new[]{ValidData1, ValidData2, ValidData3, ValidData4, ValidData5, ValidData6, ValidData7, ValidData8, ValidData9 }, 1000, ValidData9},
            new object[] {new[]{ValidData1, ValidData2, ValidData3, ValidData4, ValidData5, ValidData6, ValidData7, ValidData8 }, 10000, ValidData6},
            new object[] {new[]{ValidData1, ValidData5, ValidData7, }, 10000, null},
        };
    }
}