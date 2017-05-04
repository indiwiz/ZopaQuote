using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using ZopaQuote.DataAccess;
using ZopaQuote.Entities;
using ZopaQuote.Services;

namespace ZopaQuote
{
    internal class Program
    {
        private const string UnexpectedError = "Unexpected error occurred";
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
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
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("config.json", true, true);
            var configuration = configurationBuilder.Build();
            var appConfig = new AppConfiguration();

            appConfig.LoanAmountRange.Minimum = configuration.GetValue("AppConfiguration:LoanAmountRange:Minimum", 1000);
            appConfig.LoanAmountRange.Maximum = configuration.GetValue("AppConfiguration:LoanAmountRange:Maximum", 15000);
            appConfig.TotalNumberOfPayments = configuration.GetValue("AppConfiguration:TotalNumberOfPayments", 36);

            var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .AddSingleton<IOutputService, OutputService>()
                    .AddSingleton<IApplication, Application>()
                    .AddSingleton(appConfig)
                    .AddTransient<IFileService, FileService>()
                    .AddTransient<ICsvConverter<MarketData>, MarketDataCsvConverter>()
                    .AddSingleton<IMarketDataContext, MarketDataContext>()
                    .AddTransient<IQuoteService, QuoteService>()
                    .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddConsole(LogLevel.Error)
                .AddFile("Application.log", LogLevel.Debug);

            return serviceProvider;
        }
    }
}