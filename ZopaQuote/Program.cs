using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using ZopaQuote.Services;

namespace ZopaQuote
{
    class Program
    {
        static void Main(string[] args)
        {            
            var serviceProvider = ConfigureServices();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("Program Started.");

            var outputService = serviceProvider.GetService<IOutputService>();
            outputService.Prompt("End of Program. Press any key to exit.");
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IOutputService, OutputService>()
                    .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddFile("Application.log", LogLevel.Debug);

            return serviceProvider;
        }
    }
}