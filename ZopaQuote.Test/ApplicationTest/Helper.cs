using Moq;
using ZopaQuote.Services;

namespace ZopaQuote.Test.ApplicationTest
{
    public class Helper
    {
        public Mock<IOutputService> OutputServiceMock { get; set; }
        public Mock<IFileService> FileServiceMock { get; set; }

        public Helper()
        {
            OutputServiceMock = new Mock<IOutputService>();
            FileServiceMock = new Mock<IFileService>();
        }
        public Application GetServiceUnderTest()
        {
            return new Application(OutputServiceMock.Object,
                FileServiceMock.Object);
        }
    }
}