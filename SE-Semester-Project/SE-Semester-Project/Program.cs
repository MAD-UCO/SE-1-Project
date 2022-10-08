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

        private static Object streamLock = new object();
        public static TcpClient client;
        public static NetworkStream netStream;
        private static bool listenerSemaphore = true;
        static void Main(string[] args)
        {
            // Change this ip address to server host
            string ipAddress = "127.0.0.1";
            int port = 5005;
            client = new TcpClient(ipAddress, port);
            netStream = client.GetStream();
            String userName = loginClient(netStream);
            if (userName != "")
            {
                Console.WriteLine("Successfully connected to server");
                bool exit = false;
                string? input;
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
                            IFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(netStream, listUsers);
                            TestListActiveUsersResponse resp = (TestListActiveUsersResponse)formatter.Deserialize(netStream);
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
                            IFormatter formatter = new BinaryFormatter();
                            formatter.Serialize(netStream, smtm);
                            SimpleTextMessageResult smtr = (SimpleTextMessageResult)MessageOperator.netStreamToMessage(netStream);
                            if (smtr.sendResult == SendResult.sendSuccess)
                                Console.WriteLine("Message sent");
                            else if (smtr.sendResult == SendResult.sendFailure)
                                Console.WriteLine("The message could not be delivered");
                        }
                    }

                }

                //messageListenThread.Interrupt();
                listenerSemaphore = false;
                client.Close();
            }
            Console.WriteLine("Program exited");
        }


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
                        ClientConnectRequest loginRequest = new ClientConnectRequest(userName, password, userIsNew);
                        IFormatter formatter = new BinaryFormatter();
                        formatter.Serialize(netStream, loginRequest);
                        Console.WriteLine("Sending data...");
                        ClientConnectResponse resp = (ClientConnectResponse)formatter.Deserialize(netStream);

                        if (resp != null)
                        {
                            if (resp.response == serverConnectResponse.success)
                            {
                                _continue = false;
                            }
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

        static void messageListener()
        {
            Console.WriteLine("Starting listener...");
            while (listenerSemaphore)
            {
                if (netStream.CanRead && netStream.DataAvailable)
                {
                    MoveBitMessaging.Message msg = null;
                    lock (netStream)
                    {
                        msg = MessageOperator.netStreamToMessage(netStream);
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
                    Thread.Sleep(300);
            }
        }
    }
}