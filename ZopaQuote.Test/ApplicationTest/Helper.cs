using Microsoft.Extensions.Logging;
using Moq;
using ZopaQuote.DataAccess;
using ZopaQuote.Services;

namespace ZopaQuote.Test.ApplicationTest
{
    public class Helper
    {
        public Mock<ILoggerFactory> LoggerFactoryMock { get; set; } = new Mock<ILoggerFactory>();
        public Mock<IFileService> FileServiceMock { get; set; } = new Mock<IFileService>();
        public AppConfiguration AppConfiguration { get; set; } = new AppConfiguration();

        public Mock<IMarketDataContext> MarketDataContextMock { get; set; } = new Mock<IMarketDataContext>();

        public Helper()
        {
            LoggerFactoryMock.Setup(f => f.CreateLogger(It.IsAny<string>()))
                .Returns(new Mock<ILogger<Application>>().Object);
        }
        public Application GetServiceUnderTest()
        {
            return new Application(LoggerFactoryMock.Object,
                FileServiceMock.Object,
                AppConfiguration,
                MarketDataContextMock.Object);
        }
    }
}