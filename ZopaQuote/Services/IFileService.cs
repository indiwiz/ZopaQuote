using System;
using System.Collections.Generic;
using ZopaQuote.DataAccess;

namespace ZopaQuote.Services
{
    public interface IFileService
    {
        bool FileExists(string fileName);
        List<T> ReadCsvFile<T>(string validatedFileName, ICsvConverter<T> converter) where T : class;
    }
}