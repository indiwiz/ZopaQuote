using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using ZopaQuote.DataAccess;
using ZopaQuote.Services;

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

    public class Helper
    {
        public Mock<ILoggerFactory> LoggerFactoryMock { get; set; } = new Mock<ILoggerFactory>();

        public FileService GetServiceUnderTest()
        {
            LoggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns((new Mock<ILogger>()).Object);
            return new FileService(LoggerFactoryMock.Object);
        }

    }

    public static class DataHelper
    {
        public static MemoryStream ToMemoryStream(this string data)
        {
            var byteArray = Encoding.ASCII.GetBytes(data);
            return new MemoryStream(byteArray);
        }

        public const string ThreeLinesOfValidDataOffFive =
            @"HeaderLine
invalid data,12,svbjk
First,0.02,1000
Second,0.05,4000
invalid data
Third,0.23,10000";

        public const string ThreeLinesOfValidData =
            @"HeaderLine
First,0.02,1000
Second,0.05,4000
Third,0.23,10000";

        public const string CompleteRubbishData =
            @"HeaderLine
First,1000
Second,0.05
Third,
dsfsdvdsv
dsfvsdfs sdf sdf sdfg
s gsf
 g
sdf g
sf
 sf
g 
sf 
ad fdas f
dfdsfdsf df df
";
    }
}
