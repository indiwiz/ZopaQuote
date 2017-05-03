using System.IO;

namespace ZopaQuote.Services
{
    public class FileService : IFileService
    {
        public bool FileExists(string fileName)
        {
            return File.Exists(fileName);
        }
    }
}
