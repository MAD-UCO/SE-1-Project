using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Security.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using System.Diagnostics;
using SE_Final_Project;
using System.Windows.Forms;
using Message = SE_Final_Project.Message;
using System.IO;
using SE_Final_Project.model;

namespace SE_Semester_Project
{

    enum ClientState
    {
        NotLoggedIn             = 0,                    // No client logged in
        TryingToLogIn           = 1 << 0,               // We are trying to log into the server
        LoggedIn                = 1 << 1,               // Client is logged in
        Connected               = 1 << 2,               // Client is connected to server

        LoggedInAndConnected    = LoggedIn | Connected, // Client is logged in and connected to server
        TalkingToServer         = Connected | TryingToLogIn,
        
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
        private static List<Notification> notifications = new List<Notification>();
        public static TcpClient client;                                 // Our client connection
        public static NetworkStream netStream;                          // Our network stream to write and get messages from
        public static string myClientName = null;      // The name of this client

        private static string ipAddress = "127.0.0.1";                  // IP address of our server
        private static int portNumber = 5005;                           // The port we are connecting to
        private static bool continueLoop = false;

        private static bool runThread = false;                          // Semaphore for when the messsage listener should exit

        private static Queue<MoveBitMessage> outBoundMessages = new Queue<MoveBitMessage> ();       // Queue for messages to be sent
        private static Queue<MoveBitMessage> inBoundMessages = new Queue<MoveBitMessage> ();        // Queue for messages from the server
        private static List<MoveBitMessage> inprocessedMessages = new List<MoveBitMessage>();
        private static int maxNumberMessagesInprocessPerIteration = 5;                              // How many messages we will inprocess in one sitting
        private static int maxNumberMesssagesOutprocessPerIteration = 5;                            // How many messages we will outprocess in one sitting
        private static ClientState clientState = ClientState.NotLoggedIn;


        /// <summary>
        /// Function that hashes a given password using SHA256
        /// </summary>
        private static byte[] Sha256Hash(string password, string salt = "")
        {
            byte[] hash;
            using (SHA256 sha = SHA256.Create())
                hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password + salt));

            return hash;
        }

        /// <summary>
        /// Begins the login process of the user. This function takes a username and password and 
        /// attempts to log the user into the server.
        /// The function spins for 10 secons while waiting to communicate with the server
        /// If it gets denied or takes longer than 10 seconds to contact the server, it
        ///     will fail to login the user.
        /// </summary>
        public static bool Login(string userName, string password, bool isNew = false)
        {

            bool success = false;
            // Place login request in the outqueue
            ClientConnectRequest connectRequest = new ClientConnectRequest(userName, Sha256Hash(password), isNew);
            AddMessageToOutQueue(connectRequest);
            myClientName = userName;
            // Update client state
            SetClientState(ClientState.TryingToLogIn);
            long start = ((DateTimeOffset)(DateTime.Now)).ToUnixTimeSeconds();

            // Wait a bit while our state is 'TryingToLogIn'
            while (clientState == ClientState.TryingToLogIn)
            {
                // See if 3 or more seconds has passed - exits in case something in the message
                //  loop is taking too long to process us
                if (((DateTimeOffset)(DateTime.Now)).ToUnixTimeSeconds() - start >= 3)
                {
                    SetClientState(ClientState.NotLoggedIn);
                    notifications.Add(new Notification("Could not log in - connection to server timed out"));
                    break;
                }
                Thread.Sleep(100);
            }

            // Check to see if our state was set to 'LoggedInANdConnected'
            success = (clientState == ClientState.LoggedInAndConnected);

                
            return success;
        }

        public static List<Message> GetNewMessages()
        {
            List<Message> temp = new List<Message>();

            lock (inboundMsgQueueLock)
            {
                foreach(MoveBitMessage msg in inprocessedMessages)
                {
                    if(msg.GetType() == typeof(MediaMessage))
                    {
                        MediaMessage media = (MediaMessage)(msg);
                        Debug.Assert(media.senderFileName != null, "Media.SenderFileName is null!");
                        FileInfo fi = new FileInfo(media.senderFileName);
                        Message smilMsg = new Message("/" + fi.Name, media.smilData);
                        smilMsg.setSmilFilePath(Environment.CurrentDirectory + "/" + fi.Name);


                        foreach (KeyValuePair<string, byte[]> entry in media.videoFiles)
                        {
                            fi = new FileInfo(entry.Key);
                            File.WriteAllBytes(fi.Name, entry.Value);
                            smilMsg.AddVideoMessage(new VideoMessage(fi.Name));
                        }

                        foreach (KeyValuePair<string, byte[]> entry in media.soundFiles) 
                        {
                            fi = new FileInfo(entry.Key);
                            File.WriteAllBytes(fi.Name, entry.Value);
                            smilMsg.AddAudioMessage(new AudioMessage(fi.Name));
                        }

                        foreach(KeyValuePair<string, byte[]> entry in media.imageFiles)
                        {
                            throw new NotImplementedException();
                            /*
                            fi = new FileInfo(entry.Key);
                            File.WriteAllBytes(fi.Name, entry.Value);
                            smilMsg.Add
                            */
                        }
                        Debug.Assert(File.Exists(smilMsg.smilFilePath), $"System could not locate smilMsg.smilFilePath {smilMsg.smilFilePath}");
                        temp.Add(smilMsg);
                    }
                }
            }

            inprocessedMessages.Clear();
            return temp;
        }

        /// <summary>
        /// Function for initiating a logout
        /// </summary>
        public static void Logout()
        {
            myClientName = null;
            SetClientState(ClientState.NotLoggedIn);
            TerminateConnection();
        }

        /// <summary>
        /// Function for starting the Client Network code through
        /// </summary>
        public static void Start()
        {
            Thread messageThread = new Thread(MessengerLoop);
            messageThread.Start();
        }


        /// <summary>
        /// Function for shutting down the netcode
        /// This includes ending the message loop
        /// </summary>
        public static void Shutdown()
        {
            continueLoop = false;
            TerminateConnection();

            Thread.Sleep(100);
        }

        /// <summary>
        /// Function for retrieving the current Client state
        /// </summary>
        /// <returns></returns>
        public static ClientState GetClientState()
        {
            return clientState;
        }

        public static void SendMessage(MoveBitMessage message)
        {
            AddMessageToOutQueue(message);
        }

        public static void SendMessage(Message message)
        {

            MediaMessage mediaMessage = new MediaMessage(
                message.senderName, 
                message.receiverName, 
                message.GetSmilText(Environment.CurrentDirectory + "/" + message.smilFilePath), 
                Environment.CurrentDirectory + "/" + message.smilFilePath
             );


            Debug.Assert(mediaMessage.senderFileName != null, "senderFileName is null");
            Debug.Assert(File.Exists(mediaMessage.senderFileName), $"System did not find {mediaMessage.senderFileName}");

            foreach(VideoMessage vm in message.videoMessages)
                mediaMessage.AddFile(FileType.VideoFile, vm.filePath, File.ReadAllBytes(vm.filePath));
            foreach (AudioMessage am in message.audioMessages)
                mediaMessage.AddFile(FileType.AudioFile, am.filePath, File.ReadAllBytes(am.filePath));
            foreach (ImageMessage im in message.imageMessages)
                mediaMessage.AddFile(FileType.ImageFile, im.filePath, File.ReadAllBytes(im.filePath));

            SendMessage(mediaMessage);

        }

        /// <summary>
        /// Function for adding a given MoveBitMessage to the outprocess queue
        /// Messages placed here will be sent to the server by a worker thread
        /// </summary>
        /// <param name="message"></param>
        private static void AddMessageToOutQueue(MoveBitMessage message)
        {
            // NOTE: Don't know if we should stop the client from sending messages if
            //  if they are not connected to the server, as that could require additional
            //  saving of the queue state if the user exits the program.


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
        private static void AddMessageToInQueue(MoveBitMessage messge)
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
        /// Function for connecting to the Client to the Server given a connectRequest
        /// </summary>
        /// <param name="connectRequest"></param>
        /// <returns></returns>
        public static bool TryConnectToServer()
        {
            bool successfulConnection = false;

            client = new TcpClient(ipAddress, portNumber);
            netStream = client.GetStream();

            ClientConnectRequest request = (ClientConnectRequest)GetOutboundMessagesFromQueue(1)[0];
            ClientConnectResponse response = (ClientConnectResponse)MessageManager.WriteAndRecieveMessage(request, netStream);

            Notification result = new Notification("Something went wrong. Please try again.");
            if (response != null)
            {
                if (response.response == serverConnectResponse.success)
                    successfulConnection = true;
                else if (response.response == serverConnectResponse.invalidCredentials)
                    result.notification_message = "Your credentials are invalid, please try again";
                else if (response.response == serverConnectResponse.serverBusy)
                    result.notification_message = "The server is busy right now.";
                else if (response.response == serverConnectResponse.usernameTaken)
                    result.notification_message = "That username is taken, try another.";
                else
                    result.notification_message = "Something went wrong. Please try again";

            }
            notifications.Add(result);

            return successfulConnection;
        }

        /// <summary>
        /// Sets the connection / login state for the client
        /// </summary>
        /// <param name="newState"></param>
        private static void SetClientState(ClientState newState)
        {
            // We lost connection!
            if (clientState == ClientState.LoggedInAndConnected && newState == ClientState.LoggedIn)
                notifications.Add(new Notification("Lost connection to the server"));

            else if (clientState == ClientState.LoggedIn && newState == ClientState.LoggedInAndConnected)
                notifications.Add(new Notification("Regained connection to the server"));

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
                continueLoop = true;
                bool activity;
                while (continueLoop)
                {
                    //List<Message> incomingMessages = GetNewMessages();
                    // Someone is trying to log into the system.
                    if ((clientState & ClientState.TryingToLogIn) == ClientState.TryingToLogIn)
                    {
                        bool connectSuccess = false;
                        try
                        {
                            connectSuccess = TryConnectToServer();
                        }
                        catch (SocketException)
                        {
                            notifications.Add(new Notification("The server could not be reached at this time"));
                        }
                        finally
                        {
                            if (connectSuccess)
                                SetClientState(ClientState.LoggedInAndConnected);
                            else
                            {
                                TerminateConnection();
                                SetClientState(ClientState.NotLoggedIn);
                            }
                        }
                    }

                    // If user is not logged in or connected, sleep
                    else if ((clientState & ClientState.LoggedInAndConnected) == ClientState.NotLoggedIn)
                    {
                        Thread.Sleep(500);
                    }

                    // User is logged in but not connected to server... We need to re-establish connection.
                    else if ((clientState & ClientState.LoggedInAndConnected) == ClientState.LoggedIn)
                    {
                        // TODO:
                        //  Create new reconnect message in MoveBit-Messaging
                        //  Connect to server
                        //  Give server credentials + session ID
                        //  Re-login if session expired
                        //  Authenticate and reconnect
                        throw new NotImplementedException("Reconnecting to server after losing connection while logged in is not yet implemented");
                    }


                    // Otherwise, the user should be logged in and connected
                    else if ((clientState & ClientState.LoggedInAndConnected) == ClientState.LoggedInAndConnected)
                    {

                        //GetNewMessages();

                        activity = false;
                        // Ensure server hasn't dropped 
                        if (!ConnectionWithServerAlive())
                        {
                            // TODO: Change to be different from disconnected in the future
                            SetClientState(ClientState.LoggedIn);

                            Debug.WriteLine("Lost connection to the server");
                        }

                        else if ((clientState & ClientState.TalkingToServer) != ClientState.NotLoggedIn)
                        {
                            // We can read from the steam and something is waiting for us
                            if (netStream.CanRead && netStream.DataAvailable)
                            {
                                activity = true;
                                int inprocessed = 0;
                                MoveBitMessage incomingMessage;
                                do
                                {
                                    lock (streamLock)
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
                            foreach (MoveBitMessage msg in GetInboundMessagesFromQueue(maxNumberMesssagesOutprocessPerIteration))
                            {

                                if (msg.GetType() == typeof(InboxListUpdate))
                                {
                                    InboxListUpdate update = (InboxListUpdate)msg;
                                    foreach (MediaMessage subMsg in update.messages)
                                    {
                                        inprocessedMessages.Add(subMsg);
                                    }
                                }
                                else if (msg.GetType() == typeof(MediaMessageResponse))
                                {
                                    MediaMessageResponse result = (MediaMessageResponse)msg;
                                    if (result.result == SendResult.sendSuccess)
                                        MessageBox.Show("Your message was sent successfully");
                                    else if (result.result == SendResult.sendFailure)
                                        MessageBox.Show("You message could not be delivered");
                                }
                                else if (msg.GetType() == typeof(SimpleTextMessageResult))
                                {
                                    SimpleTextMessageResult result = (SimpleTextMessageResult)msg;
                                    if (result.sendResult == SendResult.sendSuccess)
                                        MessageBox.Show("Your message was sent successfully (THIS MESSAGE IS DEPRECATED)");
                                    else if (result.sendResult == SendResult.sendFailure)
                                        MessageBox.Show("You message could not be delivered (THIS MESSAGE IS DEPRECATED)");
                                }
                                else if (msg.GetType() == typeof(TestListActiveUsersResponse))
                                {
                                    TestListActiveUsersResponse response = (TestListActiveUsersResponse)msg;
                                    Debug.WriteLine("Server Users:");
                                    foreach (string result in response.activeUsers)
                                    {
                                        Debug.WriteLine($"\t{result}");
                                    }
                                    Debug.WriteLine("");

                                }
                                else if (msg.GetType() == typeof(SimpleTextMessage))
                                {
                                    SimpleTextMessage message = (SimpleTextMessage)msg;
                                    inprocessedMessages.Add(message);
                                    Debug.WriteLine($"New message from {message.sender}: {message.message}");
                                }
                                else if (msg.GetType() == typeof(ServerToClientLogoffCommand))
                                {
                                    Debug.WriteLine("Forced logoff message from server... Disconnecting\n");
                                    TerminateConnection();
                                }
                                else if (msg.GetType() == typeof(MediaMessage))
                                {
                                    inprocessedMessages.Add((MediaMessage)msg);
                                }
                                else
                                    Debug.WriteLine("I don't know how to process this message");
                            }

                            // Nothing to do. Just relax
                            if (!activity)
                                Thread.Sleep(250);
                        }

                    }

                    else
                        Debug.Assert(false, $"Fell out of the bottom of the message loop! State appeared to have no handle!\n: My state = '{clientState}'");
                }
                 
            }
           
            catch(Exception error)
            {
                Debug.WriteLine($"An error occured while running the MessageLoop: {error}");
            }
            finally
            {
                TerminateConnection();
            }
            return;
        }

        /// <summary>
        /// Checks to see if the server is still reachable
        /// Returns True if we are still connected
        /// </summary>
        private static bool ConnectionWithServerAlive()
        {

            return !((netStream == null)
                || !((netStream.CanRead || netStream.CanWrite) 
                || (client == null)
                || ((client.Client.Poll(1000, SelectMode.SelectRead) 
                && client.Client.Available == 0))));
        }
        
    }
}
