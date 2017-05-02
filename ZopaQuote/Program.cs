using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ZopaQuote
{
    static class Program
    {
        const string ExpectedAmountMessage = "Expected number between 1000 and 15000 and in multiple of hundreds.";
        static void Main(string[] args)
        {
            var appName = Assembly.GetEntryAssembly().GetName().Name;
            if (args.Length != 2)
            {
                ExitApplication($"Insufficient or too many arguments.",
                    $"Usage: {appName} <filename> <amount>",
                    $"Example: {appName} MarketData.csv 1200");
            }
            if (!TryGetFilePath(args[0], out var fileNameWithPath))
            {
                ExitApplication($"File not found in path {fileNameWithPath}");
            }

            var requestedAmount = GetRequestedAmount(args[1], x => ExitApplication(x, ExpectedAmountMessage));

            while (requestedAmount > 0)
            {
                var preferredOption = GetPrefferedOption(fileNameWithPath, requestedAmount);

                if (preferredOption != null)
                    $"Preferred Option is: {preferredOption}".Log();
                else
                    $"Sorry no options available".Log();

                string.Empty.Log(); string.Empty.Log(); string.Empty.Log();

                "Enter amount of loan required".Log();

                requestedAmount = GetRequestedAmount(Console.ReadLine(), x => $"{x} {ExpectedAmountMessage}".Log());
            }

            ExitApplication("End of....");
            Console.ReadLine();
        }

        private static MarketData GetPrefferedOption(string fileNameWithPath, int requestedAmount)
        {
            var marketDataList = GetMarketData(fileNameWithPath);

            marketDataList.OrderBy(x => x.Rate).ToList().ForEach(x => x.ToString().Log());

            var preferredOption = marketDataList.Where(x => x.AvailableAmount > requestedAmount).OrderBy(x => x.Rate).FirstOrDefault();
            return preferredOption;
        }

        private static int GetRequestedAmount(string arg, Action<string> action)
        {
            if (!int.TryParse(arg, out var requestedAmount))
            {
                action("Invalid argument.");
                return 0;
            }

            if (requestedAmount < 1000 || requestedAmount > 15000)
            {
                action("Amount out of range.");
                return 0;
            }

            if (requestedAmount % 100 != 0)
            {
                action("Invalid amount.");
                return 0;
            }

            return requestedAmount;
        }

        private static List<MarketData> GetMarketData(string fileNameWithPath)
        {
            var marketDataList = new List<MarketData>();
            using (var reader = LoadTextFile(fileNameWithPath))
            {
                var lineCount = 0;
                while (!reader.EndOfStream)
                {
                    string header;
                    if (lineCount == 0)
                    {
                        lineCount++;
                        header = reader.ReadLine();
                    }
                    var data = reader.ReadLine();

                    if (TryConvertToMarketData(data, out MarketData marketData))
                    {
                        marketData.LineNumber = lineCount;
                        marketDataList.Add(marketData);
                    }
                    else
                    {
                        $"Invalid data on line {lineCount}".Log();
                    }

                    lineCount++;
                }
            }
            return marketDataList;
        }

        static bool TryConvertToMarketData(string data, out MarketData marketData)
        {
            marketData = null;
            var splitData = data.Split(',');
            if (splitData.Length != 3) return false;
            if (!decimal.TryParse(splitData[1], out var rate)) return false;
            if (!long.TryParse(splitData[2], out var availableAmount)) return false;
            marketData = new MarketData() { Name = splitData[0], Rate = rate, AvailableAmount = availableAmount };
            return true;
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
    
    public class MarketData
    {
        public int LineNumber { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public long AvailableAmount { get; set; }

        public override string ToString()
        {
            return $"{LineNumber}:  Name: {Name}, Rate: {Rate}, Available Amount: {AvailableAmount}";
        }
    }
}