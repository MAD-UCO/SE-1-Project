using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{ 
    
    internal class CommandLineManager
    {
        public bool acceptOnlyLocal = true;
        public int listeningPort = 5005;

        public bool parseCommandLine(string []commandLineArgs)
        {
            bool expectArgument = false;
            bool nextShouldBeAcceptOnlyLocal = false;
            bool nextShouldBeListeningPort = false;
            string activeKey = null;
            foreach(string command in commandLineArgs)
            {
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
    
        public void echoSettings()
        {
            Console.WriteLine("The program is operating with the following variables set:");
            Console.WriteLine($"\tNetworking on localHost only: {acceptOnlyLocal}");
            Console.WriteLine($"\tListening on port #{listeningPort}");
            
            
            
            Console.WriteLine("\n");
        }
    }
}
