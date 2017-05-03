using System;
using System.IO;
using Microsoft.Extensions.Logging;
using ZopaQuote.Services;

namespace ZopaQuote
{
    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IFileService _fileService;

        public Application(
            ILoggerFactory loggerFactory,
            IFileService fileService)
        {
            _logger = loggerFactory.CreateLogger<Application>();
            _fileService = fileService;
        }

        public void Run(string[] args)
        {
            _logger.LogDebug($"Arguments: {string.Join(", ", args)}");

            if (!HasEnoughArguments(args))
            {
                throw ValidationException.InvalidArgumentException;
            }

            if (!ValidateFileName(args[0], out var validatedFileName))
            {
                throw ValidationException.InvalidFileNameException;
            }

            if (!ValidateLoanAmount(args[1], out var loanAmount))
            {
                throw ValidationException.InvalidLoanAmountException;
            }

            throw new Exception("Time pass");
        }

        private bool ValidateLoanAmount(string loanAmount, out int validatedLoanAmount)
        {
            return int.TryParse(loanAmount, out validatedLoanAmount);
        }

        private bool ValidateFileName(string fileName, out string validatedFileName)
        {
            validatedFileName = Path.Combine(AppContext.BaseDirectory, fileName);

            return _fileService.FileExists(validatedFileName);
        }

        private bool HasEnoughArguments(string[] args)
        {
            return args.Length == 2;
        }

        
    }
}
