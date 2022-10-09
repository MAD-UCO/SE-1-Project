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
    internal class CommandLineManager
    {
        // Flag for if the program should only accept localhost connections
        public bool acceptOnlyLocal = true;
        // The port number the server will operate on.
        public int listeningPort = 5005;

        /// <summary>
        /// Function meant to parse the command line and extract variables being set from it. 
        /// If a variable is unknown or invalid, this function returns false.
        /// </summary>
        /// <param name="commandLineArgs"></param>
        /// <returns></returns>
        public bool parseCommandLine(string []commandLineArgs)
        {
            bool expectArgument = false;
            bool nextShouldBeAcceptOnlyLocal = false;
            bool nextShouldBeListeningPort = false;
            string activeKey = null;
            // Iterate over every word in the command line
            foreach(string command in commandLineArgs)
            {
                // We are looking for keywords right now
                if (!expectArgument) 
                {
                    activeKey = command;
                    if (command == "acceptOnlyLocal")
                    {
                        expectArgument = true;
                        nextShouldBeAcceptOnlyLocal = true;
                    }
                    else if(command == "listeningPort")
                    {
                        expectArgument = true;
                        nextShouldBeListeningPort = true;
                    }
                    else
                    {
                        Console.WriteLine($"Unexpected command line keyword: {command}");
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
                        }
                        else if (nextShouldBeListeningPort)
                        {
                            int temp = Int16.Parse(lower);
                            if (temp < 1 || temp > 65535)
                                throw new ArgumentException($"{lower} is an invalid port number! Must be in the range of [1,65535]");
                            listeningPort = temp; 
                        }
                        else
                            throw new ArgumentException($"No processing routine for key '{activeKey}'");

                        expectArgument = false;
                    }
                    catch(Exception err)
                    {
                        Console.WriteLine($"During command processing, an error occured: {err.Message}");
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
        public void echoSettings()
        {
            Console.WriteLine("The program is operating with the following variables set:");
            Console.WriteLine($"\tNetworking on localHost only: {acceptOnlyLocal}");
            Console.WriteLine($"\tListening on port #{listeningPort}");
            
            
            
            Console.WriteLine("\n");
        }
    }
}
