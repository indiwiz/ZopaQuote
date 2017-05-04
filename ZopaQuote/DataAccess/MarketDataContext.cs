using System.Collections.Generic;
using ZopaQuote.Entities;
using ZopaQuote.Services;

namespace ZopaQuote.DataAccess
{
    public class MarketDataContext : IMarketDataContext
    {
        private readonly IFileService _fileService;
        private readonly ICsvConverter<MarketData> _csvConverter;

        public MarketDataContext(IFileService fileService,
            ICsvConverter<MarketData> csvConverter)
        {
            _fileService = fileService;
            _csvConverter = csvConverter;
        }

        public List<MarketData> MarketData { get; set; }

        public void Initialize(string fileName)
        {
            MarketData = _fileService.ReadCsvFile(fileName, _csvConverter);
        }
    }
}