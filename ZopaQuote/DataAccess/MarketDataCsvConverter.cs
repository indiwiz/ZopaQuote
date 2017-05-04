using System;
using ZopaQuote.Entities;
using ZopaQuote.Services;

namespace ZopaQuote.DataAccess
{
    public class MarketDataCsvConverter : ICsvConverter<MarketData>
    {
        public MarketData Convert(int lineNumber, string data)
        {
            var splitData = data.Split(',');
            void ThrowException(string message) => throw new ConversionException($"Error on line {lineNumber}: {message}");

            if (splitData.Length != 3)
            {
                ThrowException($"Unexpected format: {splitData}");
            }
            if (!decimal.TryParse(splitData[1], out var rate))
            {
                ThrowException($"Unable to read Rate from {splitData[1]}");
            }
            if (!int.TryParse(splitData[2], out var availableAmount))
            {
                ThrowException($"Unable to read Available Amount from {splitData[2]}");
            }

            return new MarketData()
            {
                LineNumber = lineNumber,
                Name = splitData[0],
                Rate = rate,
                AvailableAmount = availableAmount
            };
        }
    }
}