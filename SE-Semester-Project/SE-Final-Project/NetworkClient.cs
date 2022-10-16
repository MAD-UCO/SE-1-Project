using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace SE_Semester_Project
{

    enum ClientState
    {
        NotLoggedIn             = 0,                    // No client logged in
        LoggedIn                = 1 << 0,               // Client is logged in
        Connected               = 1 << 1,               // Client is connected to server

        LoggedInAndConnected    = LoggedIn | Connected, // Client is logged in and connected to server
        
    }

    /// <summary>
    /// Class for managing backend netcode responsible for connecting to the
    /// server, sending and recieving messages from it.
    /// </summary>
    internal class NetworkClient
    {
        private static Object streamLock = new Object();                // Lock for accessing the same stream for reading and writing
        private static Object inboundMsgQueueLock = new Object();       // Lock for accessing a message in the inbound queue 
        private static Object outboundMsgQueueLock = new Object();      // Lock for accessing the outbound queue
#if USE_COMMANDLINE
        private static Object writeLock = new Object();                 // Lock for writing to the console 
#endif
        public static TcpClient client;                                 // Our client connection
        public static NetworkStream netStream;                          // Our network stream to write and get messages from
        public static string myClientName = "(Unregistered User)";      // The name of this client

        private static string ipAddress = "127.0.0.1";                  // IP address of our server
        private static int portNumber = 5005;                           // The port we are connecting to

        private static bool runThread = false;                          // Semaphore for when the messsage listener should exit

        private static Queue<MoveBitMessage> outBoundMessages = new Queue<MoveBitMessage> ();       // Queue for messages to be sent
        private static Queue<MoveBitMessage> inBoundMessages = new Queue<MoveBitMessage> ();        // Queue for messages from the server
        private static int maxNumberMessagesInprocessPerIteration = 5;                              // How many messages we will inprocess in one sitting
        private static int maxNumberMesssagesOutprocessPerIteration = 5;                            // How many messages we will outprocess in one sitting
        private static ClientState clientState = ClientState.NotLoggedIn;



#if USE_COMMANDLINE
        /// <summary>
        /// Function for starting the Client Network code through CLI
        /// </summary>
        public static void start()
        {
            Console.WriteLine("Running in commandline mode - use for testing / development purposes only");
            processConsoleInterface();
            Console.WriteLine("Network client ended");
            Console.ReadLine();
        }

        /// <summary>
        /// Function for accessing the server as a client through the command line
        /// Meant for testing and develoment only
        /// </summary>
        public static void processConsoleInterface()
        {
            bool exit = false;
            string userInput;

            // Loop until we are done with the program
            while (!exit)
            {
                lock (writeLock)
                {
                    Console.Write(">>> ");
                    userInput = Console.ReadLine();
                }

                // User entered something
                if(userInput != null && userInput != "")
                {
                    userInput = userInput.ToLower();
                    if(userInput == "exit")
                    {
                        exit = true;
                        TerminateConnection();
                        Console.WriteLine("Diconnecting from server...");
                    }
                    else if(userInput == "send")
                    {

                        if (clientState == ClientState.LoggedInAndConnected)
                        {
                            Console.Write("Enter desired recipient's username: ");
                            string recipient = Console.ReadLine();
                            Console.Write("Enter the message you wish to send: ");
                            string message = Console.ReadLine();
                            SimpleTextMessage smtm = new SimpleTextMessage(recipient, myClientName, message);
                            AddMessageToOutQueue(smtm);
                        }
                        else if (clientState == ClientState.Connected)
                            throw new InvalidOperationException($"User state is illegal - registed as connected, but not logged in!");
                        else if (clientState == ClientState.NotLoggedIn)
                            Console.WriteLine("You must log in to send messages");

                    }
                    else if(userInput == "login")
                    {
                        if (clientState == ClientState.NotLoggedIn)
                        {
                            Thread netThread = new Thread(StartNetworkConnection);
                            runThread = true;
                            netThread.Start();
                        }
                        else
                            Console.WriteLine("You are already connected!");
                    }
                    else if(userInput == "logout")
                    {
                        if((clientState & ClientState.LoggedIn) != ClientState.NotLoggedIn)
                        {
                            if((clientState & ClientState.Connected) != ClientState.NotLoggedIn)
                            {
                                TerminateConnection();
                                Console.WriteLine("Diconnecting from server...");
                            }
                            Console.WriteLine("Logging out");
                        } 

                    }
                    else if(userInput == "listusers")
                    {
                        if(clientState == ClientState.LoggedInAndConnected)
                            AddMessageToOutQueue(new TestListActiveUsersRequest());
                        else if(clientState == ClientState.Connected)
                            throw new InvalidOperationException($"User state is illegal - registed as connected, but not logged in!");
                        else if (clientState != ClientState.LoggedIn)
                            Console.WriteLine("You must log in to send messages");
                    }
                }
            }

        }
#endif
        
        /// <summary>
        /// Function for retrieving the current Client state
        /// </summary>
        /// <returns></returns>
        public static ClientState GetClientState()
        {
            return clientState;
        }

        /// <summary>
        /// Function for adding a given MoveBitMessage to the outprocess queue
        /// Messages placed here will be sent to the server by a worker thread
        /// </summary>
        /// <param name="message"></param>
        public static void AddMessageToOutQueue(MoveBitMessage message)
        {
            // NOTE: Don't know if we should stop the client from sending messages if
            //  if they are not connected to the server, as that could require additional
            //  saving of the queue state if the user exits the program.
            // However, I think it's safe to throw an exception if no one is even logged in

            if (clientState == ClientState.NotLoggedIn)
                throw new InvalidOperationException("Client is not logged in or connected to server!");
            lock (outboundMsgQueueLock)
            {
                outBoundMessages.Enqueue (message);
            }
        }

        /// <summary>
        /// Function for adding a MoveBitMessage into the inprocessing queue to be
        /// processed by a worker thread.
        /// </summary>
        /// <param name="messge"></param>
        public static void AddMessageToInQueue(MoveBitMessage messge)
        {
            // NOTE: see other NOTE in 'AddMessageToOutQueue'
            if (clientState == ClientState.NotLoggedIn)
                throw new InvalidOperationException("Client is not logged in or connected to server!");
            lock (inboundMsgQueueLock)
                inBoundMessages.Enqueue (messge);
        }


        /// <summary>
        /// Function for getting a list of messages from the outbound queue for 
        /// outprocessing. 
        /// Number of messages dequed is equal to the integer passed
        /// </summary>
        /// <returns></returns>
        private static List<MoveBitMessage> GetOutboundMessagesFromQueue(int number = 1)
        {
            if (number <= 0)
                throw new ArgumentException($"The number of messages to be outprocessed is {number} - this argument must be greater than 0");

            List<MoveBitMessage> outbound = new List<MoveBitMessage>();
            lock (outboundMsgQueueLock)
            {

                for (int messageNo = 0; messageNo < Math.Min(number, outBoundMessages.Count()); messageNo++)
                    outbound.Add(outBoundMessages.Dequeue());
            }

            return outbound;
        }

        /// <summary>
        /// Function for getting a list of messages for inbound quque for inprocessing
        /// The number of messages dequeued is equal to the integer passed.
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private static List<MoveBitMessage> GetInboundMessagesFromQueue(int number = 1)
        {
            if (number <= 0)
                throw new ArgumentException($"The number of messages to be inprocessed is {number} - this argument must be greater than 0");

            List<MoveBitMessage> inbound = new List<MoveBitMessage>();

            lock (inboundMsgQueueLock)
            {
                for (int messageNo = 0; messageNo < Math.Min(number, inBoundMessages.Count()); messageNo++)
                    inbound.Add(inBoundMessages.Dequeue());
            }

            return inbound;
        }

        /// <summary>
        /// Function for starting the netcode and trying to connect to the server
        /// </summary>
        public static void StartNetworkConnection()
        {
            bool retry = true;

            try
            {
                while (retry && runThread)
                    retry = !ConnectToServer(UserLogin());
            }
            catch(SocketException)
            {
                runThread = false;
#if USE_COMMANDLINE
                Console.WriteLine("The server could not be reached at this time.\n");
#else
                throw new NotImplementedException("Alerting the user to connection failure though the GUI needs to be implemented");
#endif
                TerminateConnection();
            }

            if (runThread)
            {
#if USE_COMMANDLINE
                Console.WriteLine("Connected to server!");
#else
                throw new NotImplementedException("Alerting the user to connection success though the GUI needs to be implemented");
#endif
                SetClientState(ClientState.LoggedInAndConnected);
                MessengerLoop();
            }


        }

        /// <summary>
        /// Function for processing a user login and connecting to the server
        /// </summary>
        /// <returns></returns>
        public static ClientConnectRequest UserLogin()
        {
            // TODO: Should probably rework this so the client passes credentials to this
            //  function instead. Easier to acces via the GUI that way
            myClientName = "(Unregistered User)";
            string userName = "";
            string password = "";
            string newEntry = "";
            bool isNew = true;

#if USE_COMMANDLINE
            lock (writeLock)
            {
                Console.Write("Are you a new user [y/n]: ");
                newEntry = Console.ReadLine();
                if (newEntry != "y")
                    isNew = false;

                Console.Write("Enter a user name: ");
                userName = Console.ReadLine();
                myClientName = userName;
                Console.Write("Enter a password: ");
                password = Console.ReadLine();
            }
#else
            throw new NotImplementedException("Need to implement the initial sending of the ClientConnectRequest for the GUI still");
#endif

            byte[] hash;
            using (SHA256 sha = SHA256.Create())
                hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));

            return new ClientConnectRequest(userName, hash, isNew);

        }

        /// <summary>
        /// Function for connecting to the Client to the Server given a connectRequest
        /// </summary>
        /// <param name="connectRequest"></param>
        /// <returns></returns>
        public static bool ConnectToServer(ClientConnectRequest connectRequest)
        {
            bool successfulConnection = false;

            client = new TcpClient(ipAddress, portNumber);
            netStream = client.GetStream();
            ClientConnectResponse response = (ClientConnectResponse)MessageManager.WriteAndRecieveMessage(connectRequest, netStream);

#if USE_COMMANDLINE

            lock (writeLock)
            {
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
            }

#else

            throw new NotImplementedException("The 'ConnectToServer' function for the GUI is not implemented");

#endif

            return successfulConnection;
        }

        /// <summary>
        /// Sets the connection / login state for the client
        /// </summary>
        /// <param name="newState"></param>
        private static void SetClientState(ClientState newState)
        {
            clientState = newState;
        }

        /// <summary>
        /// Terminates the connection with the server
        /// Currently this function also logs the user out
        /// </summary>
        private static void TerminateConnection()
        {
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

            // TODO: differentiate logging out and disconnecting
            SetClientState(ClientState.NotLoggedIn);
        }
    
        /// <summary>
        /// Begin message processing loop for the client
        /// This ensures we are still connected to the server, and monitors in and out queues
        /// for messages.
        /// </summary>
        private static void MessengerLoop()
        {
            try
            {
                bool activity;
                while ((clientState & ClientState.Connected) != ClientState.NotLoggedIn)
                {
                    activity = false;
                    // Ensure server hasn't dropped 
                    if ((clientState & ClientState.Connected) == ClientState.NotLoggedIn || !(netStream.CanRead || netStream.CanWrite) ||
                        client.Client.Poll(1000, SelectMode.SelectRead) && client.Client.Available == 0)
                    {
                        // TODO: Change to be different from disconnected in the future
                        SetClientState(ClientState.NotLoggedIn);
#if USE_COMMANDLINE
                        Console.WriteLine("Lost connection to the server");
#else
                        throw new NotImplementedError("Alerting the user to sudden server-disconnect in the GUI hasn't been implemented");
#endif
                    }

                    else if (clientState == ClientState.Connected || clientState == ClientState.LoggedInAndConnected)
                    {
                        // We can read from the steam and something is waiting for us
                        if (netStream.CanRead && netStream.DataAvailable)
                        {
                            activity = true;
                            int inprocessed = 0;
                            do
                            { 
                                MoveBitMessage incomingMessage;

                                lock(streamLock)
                                    incomingMessage = MessageManager.GetMessageFromStream(netStream);

                                // FUTURE: If there is a message we recieve that we immediately need to process (e.g., the server tells
                                //  us to disconnect, do it here. Otherwise add it to the queue for later processing

                                AddMessageToInQueue(incomingMessage);

                            }
                            while (netStream.CanRead && netStream.DataAvailable && inprocessed++ < maxNumberMessagesInprocessPerIteration);
                        }

                        // We have messages to send and need to send them
                        if (netStream.CanWrite && outBoundMessages.Count() > 0)
                        {
                            activity = true;
                            int outprocessed = 0;

                            List<MoveBitMessage> outgoingMessages = GetOutboundMessagesFromQueue(maxNumberMesssagesOutprocessPerIteration);
                            foreach (MoveBitMessage outGoing in outgoingMessages)
                            {
                                // FUTURE: Same as above - if any outbound messages require us to do anythimg special put an if here for them
                                //  otherwise send the message

                                MessageManager.WriteMessageToNetStream(outGoing, netStream);
                            }
                        }


                        // Processing of inbound messages
                        foreach(MoveBitMessage msg in GetInboundMessagesFromQueue(maxNumberMesssagesOutprocessPerIteration))
                        {

#if USE_COMMANDLINE
                            if (msg.GetType() == typeof(InboxListUpdate))
                            {
                                InboxListUpdate update = (InboxListUpdate)msg;
                                lock (writeLock)
                                {
                                    Console.WriteLine("\nYou got the following new messages");
                                    foreach (SimpleTextMessage subMsg in update.messages)
                                    {
                                        Console.WriteLine($"\t{subMsg.sender} says: \"{subMsg.message}\"");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else if (msg.GetType() == typeof(SimpleTextMessageResult))
                            {
                                SimpleTextMessageResult result = (SimpleTextMessageResult)msg;
                                if (result.sendResult == SendResult.sendSuccess)
                                    Console.WriteLine("Your message was sent successfully");
                                else if (result.sendResult == SendResult.sendFailure)
                                    Console.WriteLine("Your message could not be sent");
                            }
                            else if (msg.GetType() == typeof(TestListActiveUsersResponse))
                            {
                                TestListActiveUsersResponse response = (TestListActiveUsersResponse)msg;
                                lock (writeLock)
                                {
                                    Console.WriteLine("Server Users:");
                                    foreach (string result in response.activeUsers)
                                    {
                                        Console.WriteLine($"\t{result}");
                                    }
                                    Console.WriteLine();
                                }
                            }
                            else if (msg.GetType() == typeof(SimpleTextMessage))
                            {
                                SimpleTextMessage message = (SimpleTextMessage)msg;
                                lock(writeLock)
                                    Console.WriteLine($"New message from {message.sender}: {message.message}");
                            }
                            else if (msg.GetType() == typeof(ServerToClientLogoffCommand))
                            {
                                Console.WriteLine("Forced logoff message from server... Disconnecting\n");
                                TerminateConnection();
                            }
                            else
                                Console.WriteLine("I don't know how to process this message");
                        }
#else
                        throw new NotImplementedException("Code for handling how the GUI reacts to messages requires implementation");
#endif

                        // Nothing to do. Just relax
                        if (!activity)
                            Thread.Sleep(250);
                    }
                }
                 
            }
           
            catch(Exception error)
            {
#if USE_COMMANDLINE
                Console.WriteLine($"An error occured while communicating with the server: {error}");
#else
                throw new NotImplementedException("Code for alerting to error while communicating with server hasn't been implemented");
#endif
            }
            finally
            {
                TerminateConnection();
            }
        }
    
        
    }
}
