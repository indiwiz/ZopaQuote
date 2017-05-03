using System;
using System.Reflection;

namespace ZopaQuote.Services
{
    public class OutputService : IOutputService
    {
        private static readonly string ApplicationName = Assembly.GetEntryAssembly().GetName().Name;
        public void Write(params string[] messages)
        {
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }

        public string Prompt(string message)
        {
            Console.WriteLine(Environment.NewLine + message);
            Console.Write("> ");
            return Console.ReadLine();
        }

        public void ShowHelper()
        {            
            Write($"Insufficient or too many arguments.",
                $"Usage: {ApplicationName} <filename> <amount>",
                $"Example: {ApplicationName} MarketData.csv 1200");
        }
    }
}
