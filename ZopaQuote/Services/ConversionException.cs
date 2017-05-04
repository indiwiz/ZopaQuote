using System;

namespace ZopaQuote.Services
{
    public class ConversionException : Exception
    {
        public ConversionException(string message)
            :base(message)
        {
            
        }
    }
}
