using System;

namespace ZopaQuote
{
    public class ValidationException : Exception
    {
        public const string InvalidArguments = "Insufficient or too many arguments.";
        public const string InvalidFileName = "File not found.";
        public const string InvalidLoanAmount = "Invalid loan amount, expected number in multiple of hundreds.";
        public const string LoanAmountNotInRange = "Loan amount is not in expected range.";

        private ValidationException(string message)
            : base(message)
        {
            
        }

        public ValidationException(Exception innerException)
            :base(innerException.Message, innerException)
        {

        }

        public static ValidationException InvalidArgumentException => new ValidationException(InvalidArguments);

        public static ValidationException InvalidFileNameException => new ValidationException(InvalidFileName);
        public static ValidationException InvalidLoanAmountException => new ValidationException(InvalidLoanAmount);
        public static ValidationException LoanAmountNotInRangeException(int minimum, int maximum) =>
            new ValidationException(
                new ArgumentOutOfRangeException("LoanAmount", 
                    $"{InvalidLoanAmount} Loan amount should be between {minimum} and {maximum}."));
    }
}
