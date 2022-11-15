using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
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
        level_notice,
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
        private static LogLevel level = LogLevel.level_off;                          // Current level of the logger
        private static List<string> logBuffer = new List<string>();
        private static int maxLogBufferSize = 100;
        private static string logFileName = DateTime.Now.ToString("yyMMddHHmmss") + "_server_log";
        private static Object logLock = new Object();                               // Lock to manage asynchronous calls to the logger

        /// <summary>
        /// Sets the logger to output only messages above or equal to the 
        /// importance we specify.
        /// </summary>
        /// <param name="level"></param>
        public static void SetLevel(LogLevel level)
        {
            Console.OpenStandardOutput();
            if (level != LogLevel.level_off)
                Info($"Logger level changed from '{EnumToStr(ServerLogger.level)}' --> '{EnumToStr(level)}'");
            ServerLogger.level = level;
        }

        /// <summary>
        /// Generic log function. If the given level is more Notice or 
        /// equal to what we have it set to, the message will be logged
        /// </summary>
        /// <param name="logMessage"></param>
        /// <param name="logLevel"></param>
        public static void Log(String logMessage, LogLevel logLevel)
        {
            string label = EnumToStr(logLevel) + ":";
            lock (logLock)
            {
                if (((int)logLevel) >= (int)level)
                    Console.WriteLine($"{label,-16}{logMessage}");
                logBuffer.Add($"{label,-16}{logMessage}");
            }

            if (logBuffer.Count() >= maxLogBufferSize)
                FlushLogBuffer();
            
        }

        /// <summary>
        /// Log a trace message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Trace(String logMessage)
        {
            Log(logMessage, LogLevel.level_trace);
        }

        /// <summary>
        /// Log a debug message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Debug(String logMessage)
        {
            Log(logMessage, LogLevel.level_debug);
        }

        /// <summary>
        /// Log an info message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Info(String logMessage)
        {
            Log(logMessage, LogLevel.level_info);
        }

        /// <summary>
        /// Log an Notice message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Notice(String logMessage)
        {
            Log(logMessage, LogLevel.level_notice);
        }

        /// <summary>
        /// Log a warning message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Warning(String logMessage)
        {
            Log(logMessage, LogLevel.level_warning);
        }

        /// <summary>
        /// Log an error message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Error(String logMessage)
        {
            Log(logMessage, LogLevel.level_error);
        }

        /// <summary>
        /// Log a critical error message
        /// </summary>
        /// <param name="logMessage"></param>
        public static void Critical(String logMessage)
        {
            Log(logMessage, LogLevel.level_critical);
        }

        /// <summary>
        /// Transforms an enum log level into a string for printing
        /// </summary>
        /// <param name="loglevel"></param>
        /// <returns></returns>
        private static string EnumToStr(LogLevel loglevel)
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
                    label = "NOTICE";
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
    
    
        public static void FlushLogBuffer()
        {
            lock (logLock) 
            {
                using (StreamWriter sw = File.AppendText(logFileName))
                {
                    foreach (string entry in logBuffer)
                    {
                        sw.WriteLine(entry);
                    }
                }
                logBuffer.Clear();
            }
        }
    }
}
