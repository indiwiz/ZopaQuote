﻿using Moq;
using Xunit;

namespace ZopaQuote.Test.ApplicationTest
{
    public class RunTest 
    {
        private readonly Helper _helper = new Helper();
        [Fact]
        public void Should_ThrowException_When_InsufficientArguments()
        {
            var app = _helper.GetServiceUnderTest();

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] {""}));
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

            var ex = Assert.Throws<ValidationException>(() => app.Run(new[] { "Invalid file name", string.Empty}));
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

    }
}