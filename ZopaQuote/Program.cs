using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;

namespace ZopaQuote
{
    static class Program
    {
        static void Main(string[] args)
        {
            args.Length.ToString().Log();
            var appName = Assembly.GetEntryAssembly().GetName().Name;
            if (args.Length != 2)
            {
                ExitApplication($"Insufficient or too many arguments.", $"Usage: {appName} <filename> <amount>", $"Example: {appName} MarketData.csv 1200"); 
            }
            if (!TryGetFilePath(args[0], out var fileNameWithPath))
            {
                ExitApplication($"File not found in path {fileNameWithPath}");
            }

            if (!int.TryParse(args[1], out var requestedAmount))
            {
                ExitApplication();
            }

            using (var reader = LoadTextFile(fileNameWithPath))
            {
                while (!reader.EndOfStream)
                {

                }
            }
            

            
            while (true)
            {
                
                Console.WriteLine("Enter amount of loan required");
                var loop = int.TryParse(Console.ReadLine(), out var amount);
                if (!loop) break;
            }

        }

        static void ExitApplication(params string[] messages)
        {
            string.Join(Environment.NewLine, messages).Log();
            Console.ReadLine();
            Environment.Exit(0);
        }

        static bool TryGetFilePath(string fileName, out string validatedName)
        {
            validatedName = Path.Combine(AppContext.BaseDirectory, fileName);
            return File.Exists(validatedName);
        }

        static void Log(this string message)
        {
            Console.WriteLine(message);
        }

        static StreamReader LoadTextFile(string fileName)
        {
            return File.OpenText(fileName);
        }
    }
    
}