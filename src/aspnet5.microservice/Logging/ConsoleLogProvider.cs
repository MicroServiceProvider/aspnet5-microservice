using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Framework.Logging;

namespace AspNet5.Microservice.Logging
{
    public class ConsoleLogProvider : ILoggerProvider
    {
        private readonly Func<string, LogLevel, bool> _filter;

        public ConsoleLogProvider(Func<string, LogLevel, bool> filter)
        {
            _filter = filter;
        }

        public ILogger CreateLogger(string name)
        {
            return new ConsoleLogger(name, _filter);
        }

        public void Dispose()
        {
        }

        private class ConsoleLogger : ILogger
        {
            private readonly string _name;
            private readonly Func<string, LogLevel, bool> _filter;

            public ConsoleLogger(string name, Func<string, LogLevel, bool> filter)
            {
                _name = name;
                _filter = filter ?? ((category, logLevel) => true);
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

                Console.WriteLine(ApplicationLog.FormatLogMessage(logLevel, message, _name));

            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return _filter(_name, logLevel);
            }

        }

        private class NoopDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }

    }
}
