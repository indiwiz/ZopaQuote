namespace ZopaQuote.Services
{
    public interface IOutputService
    {
        void WriteLine(string message);
        string Prompt(string message);
    }
}
