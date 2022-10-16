using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    /// <summary>
    /// Levels we may set our log and log messages to
    /// </summary>
    enum LogLevel
    {
        level_trace,
        level_debug,
        level_info,
        level_important,
        level_warning,
        level_error,
        level_critical,
        level_off,
    }

    /// <summary>
    /// A class for handling the outputing of console messages
    /// based on importance. Class is implemented as a singleton
    /// </summary>
    internal class ServerLogger
    {
        public LogLevel level;                          // Current level of the logger
        private Object logLock = new Object();          // Lock to manage asynchronous calls to the logger
        private static ServerLogger TheLogger = null;   // Singleton instance of the logger

        private ServerLogger(LogLevel level)
        {
            SetLevel(level);
        }

        /// <summary>
        /// Retrieves the singleton instance of the logger, or makes it if it does not yet exist.
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public static ServerLogger GetTheLogger(LogLevel level = LogLevel.level_trace)
        {
            if (TheLogger == null)
                TheLogger = new ServerLogger(level);

            return TheLogger;

        }

        /// <summary>
        /// Sets the logger to output only messages above or equal to the 
        /// importance we specify.
        /// </summary>
        /// <param name="level"></param>
        public void SetLevel(LogLevel level)
        {
            if(TheLogger != null)
                Info($"Logger level changed from '{EnumToStr(this.level)}' --> '{EnumToStr(level)}'");
            this.level = level;
        }

        /// <summary>
        /// Generic log function. If the given level is more important or 
        /// equal to what we have it set to, the message will be logged
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logLevel"></param>
        public void Log(String logMessage, LogLevel logLevel)
        {
            if (((int)logLevel) >= (int)level)
                lock (logLock) 
                {
                    string label = EnumToStr(logLevel) + ":";
                    Console.WriteLine($"{label, -16}{logMessage}");
                }
        }

        /// <summary>
        /// Log a trace message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Trace(String logMessage)
        {
            Log(logMessage, LogLevel.level_trace);
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Debug(String logMessage)
        {
            Log(logMessage, LogLevel.level_debug);
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Info(String logMessage)
        {
            Log(logMessage, LogLevel.level_info);
        }

        /// <summary>
        /// Log an important message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Important(String logMessage)
        {
            Log(logMessage, LogLevel.level_important);
        }

        /// <summary>
        /// Log a warning message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Warning(String logMessage)
        {
            Log(logMessage, LogLevel.level_warning);
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Error(String logMessage)
        {
            Log(logMessage, LogLevel.level_error);
        }

        /// <summary>
        /// Log a critical error message
        /// </summary>
        /// <param name="logMessage"></param>
        public void Critical(String logMessage)
        {
            Log(logMessage, LogLevel.level_critical);
        }

        /// <summary>
        /// Transforms an enum log level into a string for printing
        /// </summary>
        /// <param name="loglevel"></param>
        /// <returns></returns>
        private string EnumToStr(LogLevel loglevel)
        {
            // Probably a better way to do this...
            // But I think this works well enough
            string label = "<NONE>";
            switch ((int)loglevel)
            {
                case (0):
                    label = "TRACE";
                    break;
                case (1):
                    label = "DEBUG";
                    break;
                case (2):
                    label = "INFO";
                    break;
                case (3):
                    label = "IMPORTANT";
                    break;
                case (4):
                    label = "WARNING";
                    break;
                case (5):
                    label = "ERROR";
                    break;
                case (6):
                    label = "CRITICAL";
                    break;
                case (7):
                    label = "OFF";
                    break;
                default:
                    label = "UNDEFINED";
                    break;
            }
            return label;
        }
    }
}
