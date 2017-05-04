using Microsoft.Extensions.Logging;
using System;
using System.IO;
using ZopaQuote.DataAccess;
using ZopaQuote.Services;

namespace ZopaQuote
{
    public class Application : IApplication
    {
        private readonly ILogger _logger;
        private readonly IFileService _fileService;
        private readonly AppConfiguration _configuration;
        private readonly IMarketDataContext _marketDataContext;
        private readonly IQuoteService _quoteService;
        private readonly IOutputService _outputService;

        public Application(
            ILoggerFactory loggerFactory,
            IFileService fileService,
            AppConfiguration configuration,
            IMarketDataContext marketDataContext,
            IQuoteService quoteService,
            IOutputService outputService
            )
        {
            _logger = loggerFactory.CreateLogger<Application>();
            _configuration = configuration;
            _marketDataContext = marketDataContext;
            _quoteService = quoteService;
            _outputService = outputService;
            _fileService = fileService;
        }

        public void Run(string[] args)
        {
            _logger.LogDebug($"Arguments: {string.Join(", ", args)}");

            var (validatedFileName, loanAmount) = ValidateArguments(args);

            _marketDataContext.Initialize(validatedFileName);

            var quote = _quoteService.GetCompetitiveQuote(loanAmount);

            if (quote == null)
            {
                _outputService.Write("It is not possible to provide a quote at this time.");
            }
            else
            {
                _outputService.Write(
                    $"Requested amount: {loanAmount:C0}",
                    $"Rate: {(quote.Rate * 100):0.#}%",
                    $"Monthly repayment: {quote.MonthlyRepayment:C}",
                    $"Total repayment {quote.TotalRepayment:C}"
                    );
            }
        }

        private (string, int) ValidateArguments(string[] args)
        {
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

            if (!IsLoanAmountInRange(loanAmount))
            {
                throw ValidationException.LoanAmountNotInRangeException(_configuration.LoanAmountRange.Minimum, _configuration.LoanAmountRange.Maximum);
            }
            return (validatedFileName, loanAmount);
        }

        private bool ValidateLoanAmount(string loanAmount, out int validatedLoanAmount)
        {
            return int.TryParse(loanAmount, out validatedLoanAmount) && validatedLoanAmount % 100 == 0;
        }

        private bool IsLoanAmountInRange(int loanAmount)
        {
            return loanAmount >= _configuration.LoanAmountRange.Minimum
                && loanAmount <= _configuration.LoanAmountRange.Maximum;
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
