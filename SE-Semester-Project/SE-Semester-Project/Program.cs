using System;
using System.Net.Sockets;
using System.Text;
using System.Runtime.Serialization;
using MoveBitMessaging;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SE_Semester_Project
{
    internal static class Program
    {
        // TODO Merge winforms stuff with app stuff later
        // TODO probably put this elsewhere to avoid mixing frontend and backend stuff
        /*
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
        */

        // Lock for accessing the same stream for reading and writing
        private static Object streamLock = new object();
        public static TcpClient client;
        public static NetworkStream netStream;
        // Semaphore for when the messsage listener should exit
        private static bool listenerSemaphore = true;
        static void Main(string[] args)
        {
            // TODO: This is okay for now... Make configuration in the future?
            // Change this ip address to server host
            string ipAddress = "127.0.0.1";
            int port = 5005;
            // TODO: currently user has to be connected to the server for the lower line to work
            //  but in the future we'll need them to get farther. It also makes more sense to connect
            //  and then send the message, not connect then wait on the client to type...
            client = new TcpClient(ipAddress, port);
            netStream = client.GetStream();
            // Get the username from loginClient Function
            String userName = loginClient(netStream);
            if (userName != "")
            {
                Console.WriteLine("Successfully connected to server");
                bool exit = false;
                string? input;
                // Start the message listener thread
                Thread messageListenThread = new Thread(messageListener);
                messageListenThread.Start();

                while (!exit)
                {
                    Thread.Sleep(1000);
                    Console.Write("Enter a command >>> ");
                    input = Console.ReadLine().ToLower();

                    if (input == "exit")
                    {
                        exit = true;
                        netStream.Close();
                        client.Close();
                    }

                    else if (input == "listusers")
                    {
                        lock (streamLock)
                        {
                            TestListActiveUsersRequest listUsers = new TestListActiveUsersRequest();

                            TestListActiveUsersResponse resp = (TestListActiveUsersResponse)MessageManager.writeAndRecieveMessage(listUsers, netStream);
                            Console.WriteLine("Active users: ");
                            foreach (string activeUser in resp.activeUsers)
                                Console.WriteLine($"\t{activeUser}");

                            Console.WriteLine("\n");
                        }
                    }
                    else if (input == "send")
                    {
                        Console.Write("Enter desired recipient's username: ");
                        string recipient = Console.ReadLine();
                        Console.Write("Enter the message you wish to send: ");
                        string message = Console.ReadLine();
                        SimpleTextMessage smtm = new SimpleTextMessage(recipient, userName, message);

                        lock (streamLock)
                        {

                            SimpleTextMessageResult smtr = (SimpleTextMessageResult)MessageManager.writeAndRecieveMessage(smtm, netStream);
                            if (smtr.sendResult == SendResult.sendSuccess)
                                Console.WriteLine("Message sent");
                            else if (smtr.sendResult == SendResult.sendFailure)
                                Console.WriteLine("The message could not be delivered");
                        }
                    }

                }

                // Tell the listener thread to stop
                listenerSemaphore = false;
                client.Close();
            }
            Console.WriteLine("Program exited");
        }


        // TODO: Function is kinda broken, and honestly this whole file needs a revisit
        //  returning a string to indicate validity of logging in is jank
        /// <summary>
        /// Function for logging in a client to the server
        /// </summary>
        /// <param name="netStream"></param>
        /// <returns>If successful, the function will return the valid username</returns>
        static string loginClient(NetworkStream netStream)
        {

            bool _continue = true;
            string userName = "";

            try
            {
                while (_continue)
                {
                    bool userIsNew = false;
                    Console.Write("Are you a new user [y/n]: ");
                    string? newUser = Console.ReadLine();
                    if (newUser != null && newUser == "y")
                        userIsNew = true;

                    Console.Write("Enter a user name: ");
                    userName = Console.ReadLine();
                    Console.Write("Enter a password: ");
                    string? password = Console.ReadLine();
                    if (userName != null && password != null)
                    {
                        // Create a login request from the info we have gathered
                        ClientConnectRequest loginRequest = new ClientConnectRequest(userName, password, userIsNew);

                        // Wait for a response from the server...
                        ClientConnectResponse resp = (ClientConnectResponse)MessageManager.writeAndRecieveMessage(loginRequest, netStream);

                        if (resp != null)
                        {
                            if (resp.response == serverConnectResponse.success)
                                // TODO delete these braces
                                _continue = false;
                            else if (resp.response == serverConnectResponse.invalidCredentials)
                                Console.WriteLine("Your credentials are invalid, please try again");
                            else if (resp.response == serverConnectResponse.serverBusy)
                                Console.WriteLine("The server is busy right now.");
                            else if (resp.response == serverConnectResponse.usernameTaken)
                                Console.WriteLine("That username is taken, try another.");
                            else
                                Console.WriteLine("Something went wrong. Please try again");

                        }
                        else
                            Console.WriteLine("Something went wrong. Please try again.");
                    }
                }


            }
            catch (Exception err)
            {
                Console.WriteLine("Fatal exception: " + err.Message);
                _continue = false;
            }

            return userName;

        }


        // TODO: method name is ambiguous... Fix
        /// <summary>
        /// Function for executing on a separate thread. Simply checks to see if we
        /// have any messages for this user.
        /// </summary>
        static void messageListener()
        {
            Console.WriteLine("Starting listener...");
            while (listenerSemaphore)
            {
                // TODO: looking at this now I'm not sure the below will work... What if
                //  we get a different message from the server -> us? Could this function
                //  thet that message, robbing the primary function of it? Something to consider
                //  I think this whole implementation is a mess so find ways to better 
                //  manage it

                // If we can read the stream and something is there to read...
                if (netStream.CanRead && netStream.DataAvailable)
                {
                    MoveBitMessage msg = null;
                    lock (netStream)
                    {
                        msg = MessageManager.netStreamToMessage(netStream);
                    }


                    if (msg.GetType() == typeof(InboxListUpdate))
                    {
                        InboxListUpdate update = (InboxListUpdate)msg;
                        Console.WriteLine("You recieved the following messages:");
                        foreach (SimpleTextMessage subMsg in update.messages)
                        {
                            Console.WriteLine($"Sender : {subMsg.sender}");
                            Console.WriteLine($"Message: {subMsg.message}");
                            Console.WriteLine("");
                        }
                    }

                }
                else
                    Thread.Sleep(250);
            }
        }
    }
}