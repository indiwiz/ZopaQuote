using Xunit;
using ZopaQuote.DataAccess;
using ZopaQuote.Services;

namespace ZopaQuote.Test.MarketDataCsvConverterTest
{
    public class ConvertTest
    {
        [Fact]
        public void Should_ThrowException_When_DataFormatIsInvalid()
        {
            var data = "invalid data";

            var sut = new MarketDataCsvConverter();

            Assert.Throws(typeof(ConversionException), () => sut.Convert(0, data));
        }

        [Fact]
        public void Should_ThrowException_When_RateIsInvalid()
        {
            var data = "valid desc,invalid rate,doesn't matter";

            var sut = new MarketDataCsvConverter();

            Assert.Throws(typeof(ConversionException), () => sut.Convert(0, data));
        }

        [Fact]
        public void Should_ThrowException_When_AmountIsInvalid()
        {
            var data = "valid desc,0.043,invalid";

            var sut = new MarketDataCsvConverter();

            Assert.Throws(typeof(ConversionException), () => sut.Convert(0, data));
        }

        [Fact]
        public void Should_ReturnMarketData()
        {
            var data = "valid desc,0.043,2000";

            var sut = new MarketDataCsvConverter();

            var marketData = sut.Convert(0, data);
            Assert.NotNull(marketData);
        }
    }
}
