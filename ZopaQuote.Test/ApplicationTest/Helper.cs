using Microsoft.Extensions.Logging;
using Moq;
using ZopaQuote.Services;

namespace ZopaQuote.Test.ApplicationTest
{
    public class Helper
    {
        public Mock<ILoggerFactory> LoggerFactoryMock { get; set; }
        public Mock<IFileService> FileServiceMock { get; set; }

        public Helper()
        {
            var LoggerMock = new Mock<ILogger<Application>>();
            LoggerFactoryMock = new Mock<ILoggerFactory>();
            LoggerFactoryMock.Setup(f => f.CreateLogger(It.IsAny<string>()))
                .Returns(LoggerMock.Object);
            FileServiceMock = new Mock<IFileService>();
        }
        public Application GetServiceUnderTest()
        {
            return new Application(LoggerFactoryMock.Object,
                FileServiceMock.Object);
        }
    }
}