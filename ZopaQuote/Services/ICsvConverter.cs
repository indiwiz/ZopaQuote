namespace ZopaQuote.Services
{
    public interface ICsvConverter<out T> where T : class 
    {
        T Convert(int lineNumber, string csvData);
    }
}