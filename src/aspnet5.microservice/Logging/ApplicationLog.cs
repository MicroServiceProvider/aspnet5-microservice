using System;
using System.Threading;
using Microsoft.Framework.Logging;

namespace AspNet5.Microservice.Logging
{
    /// <summary>
    /// Static helper class to wrap around Microsoft.Framework.Logging
    /// </summary>
    public class ApplicationLog
    {
        public static readonly ILoggerFactory Factory = new LoggerFactory();

        /// <summary>
        /// Add console logger with the specified filter function
        /// <param name="filter">The filter function to pass to the console logger</param>
        /// </summary>
        public static void AddConsole(Func<string, LogLevel, bool> filter)
        {
            Factory.AddProvider(new ConsoleLogProvider(filter));
        }

        /// <summary>
        /// Add console logger with a defined minimum log level
        /// <param name="minLevel">The minimum message level to log</param>
        /// </summary>
        public static void AddConsole(LogLevel minLevel)
        {
            Factory.AddProvider(new ConsoleLogProvider((category, logLevel) => logLevel >= minLevel));
        }

        /// <summary>
        /// Add console logger with a default log level of Information
        /// </summary>
        public static void AddConsole()
        {
            Factory.AddProvider(new ConsoleLogProvider((category, logLevel) => logLevel >= LogLevel.Information));
        }

        /// <summary>
        /// Create a logger instance for the specified type
        /// </summary>
        public static ILogger CreateLogger<T>()
        {
            return Factory.CreateLogger<T>();
        }

        /// <summary>
        /// Add a file logger with a defined minimum log level
        /// <param name="minLevel">The minimum message level to log</param>
        /// </summary>
        public static void AddFile(string path, LogLevel minLevel)
        {
            Factory.AddProvider(new LogFileProvider((category, logLevel) => logLevel >= minLevel, path));
        }

        /// <summary>
        /// Create a logger instance that logs to a file
        /// </summary>
        public static void AddFile(string path)
        {
            Factory.AddProvider(new LogFileProvider((category, logLevel) => logLevel >= LogLevel.Information, path));
        }

        /// <summary>
        /// Format a log message and return it as a string
        /// <param name="logLevel">Log level of the message</param>
        /// <param name="message">The log message to format</param>
        /// <param name="loggerName">The name given of this logger</param>
        /// <returns>A string containing the formatted log message</returns>
        /// </summary>
        internal static string FormatLogMessage(LogLevel logLevel, string message, string loggerName)
        {
            // Format the message time
            string formattedDate = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss,fff");
            
            // Get a string representation of the log level
            var logLevelString = GetLogLevelString(logLevel);

            // Return the formatted message
            return $"{formattedDate} [Thread-{Thread.CurrentThread.Name}] {logLevelString}  {loggerName} - {message}";
        }

        /// <summary>
        /// Get a string representation of a specified log level
        /// </summary>
        /// <param name="logLevel">Log level</param>
        /// <returns>Returns a string representation of the specified log level</returns>
        internal static string GetLogLevelString(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Debug:
                    return "DEBUG";
                case LogLevel.Verbose:
                    return "NOTICE";
                case LogLevel.Information:
                    return "INFO";
                case LogLevel.Warning:
                    return "WARN";
                case LogLevel.Error:
                    return "ERROR";
                case LogLevel.Critical:
                    return "CRITICAL";
                default:
                    return "UNKNOWN";
            }
        }

    }
}
