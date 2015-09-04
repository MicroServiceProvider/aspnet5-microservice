using System;
using System.IO;
using Microsoft.Framework.Logging;

namespace AspNet5.Microservice.Logging
{
    
    public class LogFileProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;
        private readonly string _path;
        private readonly StreamWriter _logStreamWriter;
        private bool _disposed = false;
        private readonly string _logFile;

        public LogFileProvider(Func<string, LogLevel, bool> filter, string filePath)
        {
            _filter = filter;
            _path = filePath;
            _logStreamWriter = File.AppendText(filePath);
            _logFile = filePath;
        }

        public ILogger CreateLogger(string name)
        {
            return new FileLogger(name, _filter, _logStreamWriter, _logFile);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _logStreamWriter.Dispose();
            }
        }

        private class FileLogger : ILogger
        {
            private readonly string _name;
            private readonly Func<string, LogLevel, bool> _filter;
            private readonly object _lock = new object();
            private StreamWriter _logStreaamWriter;
            private readonly string _logFile;

            public FileLogger(string name, Func<string, LogLevel, bool> filter, StreamWriter logStreaamWriter, string logFile)
            {
                _name = name;
                _filter = filter ?? ((category, logLevel) => true);
                _logStreaamWriter = logStreaamWriter;
                _logFile = logFile;
            }

            public IDisposable BeginScopeImpl(object state)
            {
                return new NoopDisposable();
            }

            public void Log(
                LogLevel logLevel,
                int eventId,
                object state,
                Exception exception,
                Func<object, Exception, string> formatter)
            {

                if (!IsEnabled(logLevel))
                {
                    return;
                }

                var message = string.Empty;

                if (formatter != null)
                {
                    message = formatter(state, exception);
                }
                else
                {
                    message = LogFormatter.Formatter(state, exception);
                }

                if (string.IsNullOrEmpty(message))
                {
                    return;
                }

                lock (_lock)
                {
                    try
                    {
                        if (!File.Exists(_logFile))
                        {
                            _logStreaamWriter.Dispose();
                            _logStreaamWriter = File.AppendText(_logFile);
                        }
                        _logStreaamWriter.WriteLine(ApplicationLog.FormatLogMessage(logLevel, message, _name));
                        _logStreaamWriter.Flush();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Caught " + e.GetType() + ", Message: " + e.Message);
                    }
                }

            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return _filter(_name, logLevel);
            }

            private class NoopDisposable : IDisposable
            {
                public void Dispose()
                {
                }
            }

        }
    }
    
}
