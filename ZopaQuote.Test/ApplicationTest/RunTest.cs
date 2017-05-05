using Moq;
using System;
using Xunit;
using ZopaQuote.Entities;

namespace ZopaQuote.Test.ApplicationTest
{
    public class RunTest
    {
        private readonly Helper _helper = new Helper();
        [Fact]
        public void Should_ThrowException_When_InsufficientArguments()
        {
            var app = _helper.GetServiceUnderTest();

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] { "" }));
            Assert.Equal(ValidationException.InvalidArguments, ex.Message);
        }

        [Fact]
        public void Should_ThrowException_When_TooManyArguments()
        {
            var app = _helper.GetServiceUnderTest();

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] { string.Empty, string.Empty, string.Empty }));
            Assert.Equal(ValidationException.InvalidArguments, ex.Message);
        }

        [Fact]
        public void Should_ThrowException_When_FileNotFound()
        {
            var app = _helper.GetServiceUnderTest();

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] { "Invalid file name", string.Empty }));
            Assert.Equal(ValidationException.InvalidFileName, ex.Message);
        }

        [Fact]
        public void Should_ThrowException_When_InvalidLoanAmount()
        {
            _helper.FileServiceMock.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            var app = _helper.GetServiceUnderTest();

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] { "Any file name", string.Empty }));
            Assert.Equal(ValidationException.InvalidLoanAmount, ex.Message);
        }

        [Fact]
        public void Should_ThrowException_When_LoanAmountIsNotInMultipleOfHundreds()
        {
            _helper.FileServiceMock.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            var app = _helper.GetServiceUnderTest();

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] { "Any file name", "1250" }));
            Assert.Equal(ValidationException.InvalidLoanAmount, ex.Message);
        }

        [Fact]
        public void Should_ThrowException_When_LoanAmountIsNotInRange()
        {
            _helper.FileServiceMock.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            _helper.AppConfiguration.LoanAmountRange.Minimum = 100;
            _helper.AppConfiguration.LoanAmountRange.Maximum = 1000;
            var app = _helper.GetServiceUnderTest();

            var exception = Assert.Throws<ValidationException>(() => app.Run(new[] { "Any file name", "1200" }));
            Assert.IsType<ArgumentOutOfRangeException>(exception.InnerException);
        }

        [Fact]
        public void Should_PrintQuote_When_QuoteFound()
        {
            _helper.FileServiceMock.Setup(s => s.FileExists(It.IsAny<string>())).Returns(true);
            _helper.QuoteServiceMock.Setup(q => q.GetCompetitiveQuote(It.IsAny<int>())).Returns(new[] { new Quote(string.Empty, 1, 2, 3) });
            _helper.AppConfiguration.LoanAmountRange.Minimum = 100;
            _helper.AppConfiguration.LoanAmountRange.Maximum = 1000;

            var app = _helper.GetServiceUnderTest();

            app.Run(new[] { "Any file name", "200" });
            _helper.OutputServiceMock.Verify(o => o.Write(It.IsAny<string[]>()));
        }
    }
}
