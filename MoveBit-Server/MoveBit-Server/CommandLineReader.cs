using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    /// <summary>
    /// Class for managing functions for parinsing command line arguments.
    /// Meant to help wiht simple configuration variables set at launch time.
    /// </summary>
    internal class CommandLineReader
    {
        public bool acceptOnlyLocal = true;                 // Flag for if the program should only accept localhost connections
        public int listeningPort = 5005;                    // The port number the server will operate on.
        public bool kickIdleUsers = false;                  // TEST variable for kicking inactive users
        public string logLevel = "TRACE";                   // Log level string to tell what log level we are at in echo
        public LogLevel level = LogLevel.level_trace;       // The log level we set from the CLI

        /// <summary>
        /// Function meant to parse the command line and extract variables being set from it. 
        /// If a variable is unknown or invalid, this function returns false.
        /// </summary>
        /// <param name="commandLineArgs"></param>
        /// <returns></returns>
        public bool ParseCommandLine(string[] commandLineArgs)
        {
            bool expectArgument = false;
            bool nextShouldBeAcceptOnlyLocal = false;
            bool nextShouldBeListeningPort = false;
            bool nextShouldBeLogLevel = false;
            bool nextShouldBeKick = false;
            string activeKey = null;
            string lowerCommand = null;
            // Iterate over every word in the command line
            foreach (string command in commandLineArgs)
            {
                // We are looking for keywords right now
                if (!expectArgument)
                {
                    lowerCommand = command.ToLower();
                    activeKey = command;
                    if (lowerCommand == "acceptonlylocal")
                    {
                        expectArgument = true;
                        nextShouldBeAcceptOnlyLocal = true;
                    }
                    else if (lowerCommand == "listeningport")
                    {
                        expectArgument = true;
                        nextShouldBeListeningPort = true;
                    }
                    else if (lowerCommand == "kickidle")
                    {
                        expectArgument = true;
                        nextShouldBeKick = true;
                    }
                    else if (lowerCommand == "loglevel")
                    {
                        expectArgument = true;
                        nextShouldBeLogLevel = true;
                    }
                    else
                    {
                        ServerLogger.Error($"Unexpected command line keyword: {command}");
                        return false;
                    }
                }
                // We are looking for an argument and found something that is not an empty string
                else if (command != "")
                {
                    string lower = command.ToLower();
                    try
                    {
                        if (nextShouldBeAcceptOnlyLocal)
                        {
                            if (lower == "true")
                                acceptOnlyLocal = true;
                            else if (lower == "false")
                                acceptOnlyLocal = false;
                            else
                                throw new ArgumentException($"Invalid argument for keyword '{activeKey}' - {lower}");
                            nextShouldBeAcceptOnlyLocal = false;
                        }
                        else if (nextShouldBeListeningPort)
                        {
                            int temp = Int16.Parse(lower);
                            if (temp < 1 || temp > Int16.MaxValue)
                                throw new ArgumentException($"{lower} is an invalid port number! Must be in the range of [1,65535]");
                            listeningPort = temp;

                            nextShouldBeListeningPort = false;
                        }
                        else if (nextShouldBeKick)
                        {
                            if (lower == "true")
                                kickIdleUsers = true;
                            else if (lower == "false")
                                kickIdleUsers = false;
                            else
                                throw new ArgumentException($"Invalid argument for keyword '{activeKey}' - {lower}");
                        }
                        else if (nextShouldBeLogLevel)
                        {
                            if (lower == "trace")
                            {
                                level = LogLevel.level_trace;
                                logLevel = "TRACE";
                            }
                            else if (lower == "debug")
                            {
                                level = LogLevel.level_debug;
                                logLevel = "DEBUG";
                            }
                            else if (lower == "info")
                            {
                                level = LogLevel.level_info;
                                logLevel = "INFO";
                            }
                            else if (lower == "notice")
                            {
                                level = LogLevel.level_notice;
                                logLevel = "NOTICE";
                            }
                            else if (lower == "warning")
                            {
                                level = LogLevel.level_warning;
                                logLevel = "WARNING";
                            }
                            else if (lower == "error")
                            {
                                level = LogLevel.level_error;
                                logLevel = "ERROR";
                            }
                            else if (lower == "critical")
                            {
                                level = LogLevel.level_critical;
                                logLevel = "CRITICAL";
                            }
                            else if (lower == "off" || lower == "none" || lower == "disable")
                            {
                                level = LogLevel.level_off;
                                logLevel = "OFF";
                            }
                            else
                                throw new ArgumentException($"Invalid argument for keyword '{activeKey}' - {lower}");

                            ServerLogger.SetLevel(level);
                        }
                        else
                            throw new ArgumentException($"No processing routine for key '{activeKey}'");

                        expectArgument = false;
                    }
                    catch (Exception err)
                    {
                        ServerLogger.Error($"During command processing, an error occured: {err.Message}");
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Function for displaying each of the command line variables
        /// and what they are set to.
        /// </summary>
        public void EchoSettings()
        {
            ServerLogger.Info("The program is operating with the following variables set:");
            ServerLogger.Info($"Networking on localHost only: {acceptOnlyLocal}");
            ServerLogger.Info($"Listening on port #{listeningPort}");
            ServerLogger.Info($"Kicking idle users: {kickIdleUsers}");
            ServerLogger.Info($"Set Log level: {logLevel}\n");
        }
    }
}
