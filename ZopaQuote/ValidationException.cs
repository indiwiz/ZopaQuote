using System;

namespace ZopaQuote
{
    public class ValidationException : Exception
    {
        public const string InvalidArguments = "Insufficient or too many arguments.";
        public const string InvalidFileName = "File not found.";
        public const string InvalidLoanAmount = "Invalid loan amount, expected number.";

        private ValidationException(string message)
            : base(message)
        {
            
        }

        public static ValidationException InvalidArgumentException => new ValidationException(InvalidArguments);

        public static ValidationException InvalidFileNameException => new ValidationException(InvalidFileName);
        public static ValidationException InvalidLoanAmountException => new ValidationException(InvalidLoanAmount);
    }
}
