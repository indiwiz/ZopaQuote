using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;

namespace ZopaQuote.Services
{
    public class FileService : IFileService
    {
        private readonly ILogger<FileService> _logger;

        public FileService(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<FileService>();
        }

        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }

        public List<T> ReadCsvFile<T>(string validatedFileName, ICsvConverter<T> converter) where T : class
        {
            using (var reader = File.OpenText(validatedFileName))
            {
                return ReadCsvFile(reader, converter);
            }
        }

        public List<T> ReadCsvFile<T>(StreamReader reader, ICsvConverter<T> converter) where T : class
        {
            var dataList = new List<T>();
            var lineCount = 0;
            while (!reader.EndOfStream)
            {
                var csvData = reader.ReadLine();
                lineCount++;
                if (lineCount > 10 && dataList.Count == 0)
                {
                    throw new Exception("Too many errors occured trying to read file.");
                }
                if (lineCount == 1)
                {
                    //Assume header line and ignore
                    continue;
                }

                try
                {
                    var data = converter.Convert(lineCount, csvData);
                    dataList.Add(data);
                }
                catch (Exception)
                {
                    _logger.LogWarning($"Invalid data on line {lineCount}");
                }
            }
            return dataList;
        }
    }
}
