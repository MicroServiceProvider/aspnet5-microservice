namespace AspNet5.Microservice.Logging
{
    public interface IApplicationLogProvider
    {
        void WriteLog(string message, LogLevel level);
        bool IsEnabled(LogLevel level);
    }
}
