using Microsoft.Extensions.Logging;
using Moq;
using ZopaQuote.Services;

namespace ZopaQuote.Test.FileServiceTest
{
    public class Helper
    {
        public Mock<ILoggerFactory> LoggerFactoryMock { get; set; } = new Mock<ILoggerFactory>();

        public FileService GetServiceUnderTest()
        {
            LoggerFactoryMock.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns((new Mock<ILogger>()).Object);
            return new FileService(LoggerFactoryMock.Object);
        }

    }
}