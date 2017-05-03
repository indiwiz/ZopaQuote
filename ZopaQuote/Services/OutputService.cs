using System;

namespace ZopaQuote.Services
{
    public class OutputService : IOutputService
    {
        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public string Prompt(string message)
        {
            Console.WriteLine(message);
            return Console.ReadLine();
        }
    }
}
