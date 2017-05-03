using System.IO;
using ZopaQuote.Services;

namespace ZopaQuote
{
    public class Application : IApplication
    {
        private readonly IOutputService _outputService;

        public Application(IOutputService outputService)
        {
            _outputService = outputService;
        }

        public void Run(string[] args)
        {
            _outputService.Write(args);

            if (!HasEnoughArguments(args))
            {
                _outputService.ShowHelper();
                return;
            }

        }

        
        private bool HasEnoughArguments(string[] args)
        {
            return args.Length == 2;
        }

        
    }
}
