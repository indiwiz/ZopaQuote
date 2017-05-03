using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace ZopaQuote
{
    class Program
    {
        static void Main(string[] args)
        {

            var serviceCollection = new ServiceCollection();

            var serviceProvider = ConfigureServices(serviceCollection);

            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<Program>();

            logger.LogDebug("Logging activated");
            logger.LogInformation("test Logging ");
            Console.WriteLine("End of Program. Press any key to exit.");
            Console.ReadKey();
        }

        private static IServiceProvider ConfigureServices(IServiceCollection serviceCollection)
        {
            var serviceProvider = new ServiceCollection()
                    .AddLogging()
                    .BuildServiceProvider();

            serviceProvider
                .GetService<ILoggerFactory>()
                .AddFile("Application.log", LogLevel.Debug);

            return serviceProvider;
        }
    }
}