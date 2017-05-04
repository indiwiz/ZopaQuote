using System;
using System.IO;
using System.Linq;
using Xunit;
using ZopaQuote.DataAccess;

namespace ZopaQuote.Test.FileServiceTest
{
    public class ReadCsvFileTest
    {
        private readonly Helper _helper = new Helper();
        [Fact]
        public void Should_ReturnConvertedData_Except_FirstLine()
        {
            using (var stream = DataHelper.ThreeLinesOfValidData.ToMemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var sut = _helper.GetServiceUnderTest();
                    var list = sut.ReadCsvFile(reader, new MarketDataCsvConverter());
                    Assert.Equal(3, list.Count);
                    Assert.Equal(2, list.First().LineNumber);
                    Assert.Equal(4, list.Last().LineNumber);
                }
            }
        }

        [Fact]
        public void Should_ThrowException_When_InvalidData()
        {
            using (var stream = DataHelper.CompleteRubbishData.ToMemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var sut = _helper.GetServiceUnderTest();
                    Assert.Throws(typeof(Exception),
                        () => sut.ReadCsvFile(reader, new MarketDataCsvConverter()));
                }
            }
        }

        [Fact]
        public void Should_ReturnConvertedData_Except_InvalidLines()
        {
            using (var stream = DataHelper.ThreeLinesOfValidDataOffFive.ToMemoryStream())
            {
                using (var reader = new StreamReader(stream))
                {
                    var sut = _helper.GetServiceUnderTest();
                    var list = sut.ReadCsvFile(reader, new MarketDataCsvConverter());
                    Assert.Equal(3, list.Count);
                    Assert.Equal(3, list.First().LineNumber);
                    Assert.Equal(6, list.Last().LineNumber);
                }
            }
        }
    }
}
