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

            var application = serviceProvider.GetService<IApplication>();
            application.Run(args);

            var outputService = serviceProvider.GetService<IOutputService>();
            outputService.Prompt("End of Program. Press 'Enter' key to exit.");
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IOutputService, OutputService>()
                    .AddSingleton<IApplication, Application>()
                    .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddFile("Application.log", LogLevel.Debug);

            return serviceProvider;
        }
    }
}