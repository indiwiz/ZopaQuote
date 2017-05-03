using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using ZopaQuote.Services;

namespace ZopaQuote
{
    class Program
    {
        private const string UnexpectedError = "Unexpected error occurred";
        static void Main(string[] args)
        {            
            var serviceProvider = ConfigureServices();

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();
            logger.LogInformation("Program Started.");

            var application = serviceProvider.GetService<IApplication>();
            var outputService = serviceProvider.GetService<IOutputService>();

            try
            {
                application.Run(args);
            }
            catch (ValidationException e)
            {
                outputService.ShowHelper(e.Message);
            }
            catch (Exception ex)
            {
                //Global error handling, requires refactoring post .net Core 1.2
                logger.LogError(0, ex, UnexpectedError);
                outputService.Write(UnexpectedError);
            }

            
            outputService.Prompt("End of Program. Press 'Enter' key to exit.");
        }

        private static IServiceProvider ConfigureServices()
        {
            var serviceProvider = new ServiceCollection()
                    .AddLogging()                    
                    .AddSingleton<IOutputService, OutputService>()
                    .AddSingleton<IApplication, Application>()
                    .AddTransient<IFileService, FileService>()
                    .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Error)
                .AddFile("Application.log", LogLevel.Debug);

            return serviceProvider;
        }        
    }
}