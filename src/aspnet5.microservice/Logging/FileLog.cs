using System.IO;

namespace AspNet5.Microservice.Logging
{
    public class FileLog : IApplicationLogProvider
    {
        private readonly LogLevel _minLevel;
        private FileStream _logFileStream;
        private readonly object _lock = new object();
        private readonly string _logFile;

        public FileLog(string filePath, LogLevel minLevel = LogLevel.Info)
        {
            _logFileStream = new FileStream(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            _minLevel = minLevel;
            _logFile = filePath;
        }

        public void WriteLog(string message, LogLevel level)
        {
            // Check if the specified log level is enabled
            if (!IsEnabled(level))
            {
                return;
            }

            // Don't bother with empty messages
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            lock (_lock)
            {
                if (!File.Exists(_logFile) || !_logFileStream.CanWrite)
                {
                    // File has been delete so dispose the stream and create a new one
                    _logFileStream.Dispose();
                    _logFileStream = new FileStream(_logFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                }

                using (StreamWriter sw = new StreamWriter(_logFileStream))
                {
                    // Write to the file
                    sw.WriteLine(message);

                    // Flush the contents to disk
                    sw.Flush();
                }

            }

        }

        public bool IsEnabled(LogLevel level)
        {
            return (level >= _minLevel);
        }

    }
}
