using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SE_Semester_Project
{
    internal class NetworkClient
    {
        // Lock for accessing the same stream for reading and writing
        private static Object streamLock = new Object();
        private static Object inboundMsgQueueLock = new Object();
        private static Object outboundMsgQueueLock = new Object();
        private static Object writeLock = new Object();
        public static TcpClient client;
        public static NetworkStream netStream;
        public static string myClientName = "(Unregistered User)";

        private static string ipAddress = "127.0.0.1";
        private static int portNumber = 5005;

        // Semaphore for when the messsage listener should exit
        private static bool runThread = false;

        public static bool connectedToServer = false;

        private static Queue<MoveBitMessage> outBoundMessages = new Queue<MoveBitMessage> ();
        private static Queue<MoveBitMessage> inBoundMessages = new Queue<MoveBitMessage> ();
        private static int maxNumberMessagesInprocessPerIteration = 5;
        private static int maxNumberMesssagesOutprocessPerIteration = 5;


        public static void start()
        {
            processConsoleInterface();
            Console.WriteLine("Network client ended");
            Console.ReadLine();
        }

        // This function is here until we link this up with the GUI
        public static void processConsoleInterface()
        {
            bool exit = false;
            string userInput;
            while (!exit)
            {
                lock (writeLock)
                {
                    Console.Write(">>> ");
                    userInput = Console.ReadLine();
                }

                if(userInput != null && userInput != "")
                {
                    userInput = userInput.ToLower();
                    if(userInput == "exit")
                    {
                        exit = true;
                        terminateConnection();
                        Console.WriteLine("Diconnecting from server...");
                    }
                    else if(userInput == "send")
                    {
 
                        Console.Write("Enter desired recipient's username: ");
                        string recipient = Console.ReadLine();
                        Console.Write("Enter the message you wish to send: ");
                        string message = Console.ReadLine();
                        SimpleTextMessage smtm = new SimpleTextMessage(recipient, myClientName, message);
                        addMessageToOutQueue(smtm);

                    }
                    else if(userInput == "login" && !connectedToServer)
                    {
                        Thread netThread = new Thread(startNetworkConnection);
                        runThread = true;
                        netThread.Start();
                        Thread.Sleep(100);
                    }
                    else if(userInput == "logout" && connectedToServer)
                    {
                        terminateConnection();
                        Console.WriteLine("Diconnecting from server...");
                    }
                    else if(userInput == "listusers")
                    {
                        addMessageToOutQueue(new TestListActiveUsersRequest());
                    }
                }
            }

        }

        public static void addMessageToOutQueue(MoveBitMessage message)
        {
            lock (outboundMsgQueueLock)
            {
                outBoundMessages.Enqueue (message);
            }
        }

        public static void addMessageToInQueue(MoveBitMessage messge)
        {
            lock (inboundMsgQueueLock)
            {
                inBoundMessages.Enqueue (messge);
            }
        }

        public static MoveBitMessage getNextOutboundMessageFromQueue()
        {
            MoveBitMessage outbound;
            lock (outboundMsgQueueLock)
            {
                outbound = outBoundMessages.Dequeue();
            }

            return outbound;
        }

        public static MoveBitMessage getNextInboutMessageFromQueue()
        {
            MoveBitMessage inbound;
            lock (inboundMsgQueueLock)
            {
                inbound = inBoundMessages.Dequeue();
            }

            return inbound;
        }

        public static void startNetworkConnection()
        {
            bool retry = true;

            try
            {
                lock (writeLock)
                {
                    while (retry && runThread)
                        retry = !connectToServer(userLogin());
                }
            }
            catch(SocketException err)
            {
                runThread = false;
                Console.WriteLine("The server could not be reached at this time.\n");
                terminateConnection();
            }

            if (runThread)
            {
                Console.WriteLine("Connected to server!");
                connectedToServer = true;
                messengerLoop();
            }


        }

        public static ClientConnectRequest userLogin()
        {
            myClientName = "(Unregistered User)";
            string userName = "";
            string password = "";
            string newEntry = "";
            bool isNew = true;

            Console.Write("Are you a new user [y/n]: ");
            newEntry = Console.ReadLine();
            if (newEntry != "y")
                isNew = false;

            Console.Write("Enter a user name: ");
            userName = Console.ReadLine();
            myClientName = userName;
            Console.Write("Enter a password: ");
            password = Console.ReadLine();

            return new ClientConnectRequest(userName, password, isNew);

        }

        public static bool connectToServer(ClientConnectRequest connectRequest)
        {
            bool successfulConnection = false;

            client = new TcpClient(ipAddress, portNumber);
            netStream = client.GetStream();
            ClientConnectResponse response = (ClientConnectResponse)MessageManager.writeAndRecieveMessage(connectRequest, netStream);
            if (response != null)
            {
                if (response.response == serverConnectResponse.success)
                    successfulConnection = true;
                else if (response.response == serverConnectResponse.invalidCredentials)
                    Console.WriteLine("Your credentials are invalid, please try again");
                else if (response.response == serverConnectResponse.serverBusy)
                    Console.WriteLine("The server is busy right now.");
                else if (response.response == serverConnectResponse.usernameTaken)
                    Console.WriteLine("That username is taken, try another.");
                else
                    Console.WriteLine("Something went wrong. Please try again");

            }
            else
                Console.WriteLine("Something went wrong. Please try again.");



            return successfulConnection;
        }

        private static void terminateConnection()
        {
            connectedToServer = false;
            lock (streamLock)
            {
                if (netStream != null)
                {
                    netStream.Close();
                    netStream = null;
                }
                if (client != null)
                {
                    client.Close();
                    client = null;
                }
            }
        }
    
        private static void messengerLoop()
        {
            try
            {
                bool activity;
                while (connectedToServer)
                {
                    activity = false;
                    // Ensure server hasn't dropped 
                    if (connectedToServer && client.Client.Poll(1000, SelectMode.SelectRead) && client.Client.Available == 0)
                    {
                        connectedToServer = false;
                        Console.WriteLine("Lost connection to the server");
                    }
                    else if(connectedToServer)
                    {
                        // We can read from the steam and something is waiting for us
                        if (netStream.CanRead && netStream.DataAvailable)
                        {
                            activity = true;
                            int inprocessed = 0;
                            do
                            {
                                MoveBitMessage incomingMessage = MessageManager.netStreamToMessage(netStream);

                                // FUTURE: If there is a message we recieve that we immediatly need to process (e.g., the server tells
                                //  us to disconnect, do it here. Otherwise add it to the queue for later processing

                                addMessageToInQueue(incomingMessage);

                            }
                            while (netStream.CanRead && netStream.DataAvailable && inprocessed++ < maxNumberMessagesInprocessPerIteration);
                        }

                        // We have messages to send and need to send them
                        if (netStream.CanWrite && outBoundMessages.Count() > 0)
                        {
                            activity = true;
                            int outprocessed = 0;
                            do
                            {
                                MoveBitMessage outgoingMessage = getNextOutboundMessageFromQueue();
                                // FUTURE: Same as above - if any outbound messages require us to do anythign special put an if here for them
                                //  otherwise send the message

                                MessageManager.writeMessageToNetStream(outgoingMessage, netStream);

                            }
                            while (netStream.CanRead && netStream.DataAvailable && outprocessed++ < maxNumberMessagesInprocessPerIteration);
                        }

                        while (inBoundMessages.Count() > 0)
                        {
                            MoveBitMessage msg = getNextInboutMessageFromQueue();

                            if(msg.GetType() == typeof(InboxListUpdate))
                            {
                                InboxListUpdate update = (InboxListUpdate)msg;
                                lock (writeLock)
                                {
                                    Console.WriteLine("\nYou got the following new messages");
                                    foreach(SimpleTextMessage subMsg in update.messages)
                                    {
                                        Console.WriteLine($"\t{subMsg.sender} says: \"{subMsg.message}\"");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else if(msg.GetType() == typeof(SimpleTextMessageResult))
                            {
                                SimpleTextMessageResult result = (SimpleTextMessageResult)msg;
                                if (result.sendResult == SendResult.sendSuccess)
                                    Console.WriteLine("Your message was sent successfully");
                                else if (result.sendResult == SendResult.sendFailure)
                                    Console.WriteLine("Your message could not be sent");
                            }
                            else if(msg.GetType() == typeof(TestListActiveUsersResponse))
                            {
                                TestListActiveUsersResponse response = (TestListActiveUsersResponse)msg;
                                lock (writeLock) {
                                    Console.WriteLine("Server Users:");
                                    foreach (string result in response.activeUsers)
                                    {
                                        Console.WriteLine($"\t{result}");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else if(msg.GetType() == typeof(ServerToClientLogoffCommand))
                            {
                                Console.WriteLine("Forced logoff message from server... Disconnecting\n");
                                terminateConnection();
                            }
                        }

                        // Nothing to do. Just relax
                        if (!activity)
                            Thread.Sleep(250);
                    }
                }
                 
            }
           
            catch(Exception error)
            {
                Console.WriteLine($"An error occured while communicating with the server: {error}");
            }
            finally
            {
                terminateConnection();
            }
        }
    
        
    }
}
