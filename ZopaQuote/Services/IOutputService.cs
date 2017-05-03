namespace ZopaQuote.Services
{
    public interface IOutputService
    {
        void Write(params string[] messages);
        string Prompt(string message);
        void ShowHelper();
    }
}
