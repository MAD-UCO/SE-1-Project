using MoveBit_Server;
using System.Runtime.Serialization;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using MoveBitMessaging;
using System.Diagnostics.Tracing;

class MoveBitServer
{
    public static void Main(string[] args)
    {
        // Load local databases
        UserDatabase userDatabase = UserDatabase.GetTheDatabase();
        userDatabase.loadDatabase();
        /*
        LoginService loginService = new LoginService();
        loginService.Start();
        loginService.waitToEnd();
        */


        CommandLineManager clim = new CommandLineManager();

        // Try to parse any/all command line arguments
        if (clim.parseCommandLine(args))
        {
            clim.echoSettings();
            Console.WriteLine("//Starting MoveBit Server//");
            bool runServer = true;
            TcpListener server;
            TcpClient client;

            // Run in localhost mode
            if (clim.acceptOnlyLocal)
            {
                server = new TcpListener(System.Net.IPAddress.Loopback, clim.listeningPort);
                Console.WriteLine("Server listening on loopback only");
            }

            // Accept any IP connection
            else
            {
                server = new TcpListener(System.Net.IPAddress.Any, clim.listeningPort);
                Console.WriteLine("Server listening on all interfaces");
            }

            server.Start();
            try
            {
                while (runServer)
                {
                    client = server.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(processUser, client);
                }
            }
            catch(Exception exception)
            {
                Console.WriteLine($"An error occured while running the server: {exception.Message}");
                runServer = false;
            }

        }
        // Something went wrong while parsing the command line...
        else
            Console.WriteLine("The command line arguments could not be set");

        Console.WriteLine("Shutting down the server...");
    }


    public static void processUser(Object clientObj)
    {
        Console.WriteLine($"\tNew client connected");
        TcpClient client = (TcpClient)clientObj;
        NetworkStream netStream = client.GetStream();
        UserAccount user = null;
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            string name = userLogin(netStream);
            if (name != null)
            {
                user = UserDatabase.GetTheDatabase().getUser(name);
                user.setOnline();

                bool endConnetion = false;
                while (!endConnetion)
                {
                    if (netStream.DataAvailable)
                    {
                        Message m = MessageOperator.netStreamToMessage(netStream);
                        if (m.GetType() == typeof(TestListActiveUsersRequest))
                        {
                            List<string> activeUsers;
                            TestListActiveUsersResponse response = new TestListActiveUsersResponse(UserDatabase.GetTheDatabase().getActiveUsers());
                            formatter.Serialize(netStream, response);
                        }
                        else if(m.GetType() == typeof(SimpleTextMessage))
                        {
                            SimpleTextMessage message = (SimpleTextMessage)m;
                            SimpleTextMessageResult result;
                            UserDatabase userDatabase = UserDatabase.GetTheDatabase();
                            if (userDatabase.userExists(message.recipient))
                            {
                                UserAccount userAccount = userDatabase.getUser(message.recipient);
                                userAccount.addMessageToInbox(message);
                                result = new SimpleTextMessageResult(SendResult.sendSuccess);
                            }
                            else
                                result = new SimpleTextMessageResult(SendResult.sendFailure);

                            formatter.Serialize(netStream, result);
                        }
                    }
                    else if (client.Client.Poll(1000, SelectMode.SelectRead)) 
                    {
                        endConnetion = true;
                        Console.WriteLine($"\t{user.userName} disconnected");
                        netStream.Close();
                    }
                    else if (user.hasUnreadMessages() && netStream.CanWrite)
                    {
                        List<Message> messages = user.getUnreadMessages();
                        Console.Write($"\tSending {user.userName} their {messages.Count()} new messages");
                        InboxListUpdate update = new InboxListUpdate(messages);
                        formatter.Serialize(netStream, update);
                    }

                    Thread.Sleep(250);
                }
            }
        }
        catch(Exception exception)
        {
            string clientName = (user == null) ? "Unknown User" : user.userName;
            Console.WriteLine($"\tAn exception occured whilst communicating with {clientName}: {exception.ToString()}");
        }
        finally
        {
            if (user != null)
                user.setOffline();
            client.Close();
        }

    }

    public static string userLogin(NetworkStream netStream)
    {
        string loggedUser = null;
        BinaryFormatter formatter = new BinaryFormatter();
        ClientConnectRequest connectRequest = (ClientConnectRequest)formatter.Deserialize(netStream);
        ClientConnectResponse connectResponse;

        UserDatabase db = UserDatabase.GetTheDatabase();
        // User didn't provide a username or password...
        if (connectRequest.userName == "" || connectRequest.password == "")
        {
            connectResponse = new ClientConnectResponse(serverConnectResponse.invalidCredentials);
        }
        // User trying to create a new account
        else if (connectRequest.createAccountFlag)
        {
            // Username taken
            if (db.userExists(connectRequest.userName))
            {
                connectResponse = new ClientConnectResponse(serverConnectResponse.usernameTaken);
            }

            // Insert additional cases here...
            // else if() ...

            // User able to create account
            else if(db.insertUserIfNotTaken(connectRequest.userName, connectRequest.password))
            {
                // Inserted into database
                connectResponse = new ClientConnectResponse(serverConnectResponse.success);
                Console.WriteLine($"\tRegistered new user: {connectRequest.userName}");
                loggedUser = connectRequest.userName;
            }
            // Unable to insert... 
            else
            {
                connectResponse = new ClientConnectResponse(serverConnectResponse.usernameTaken);
            }
        }
        // User trying to log into existing account
        else
        {
            // Check if username or password invalid
            if(!db.userExists(connectRequest.userName) || !db.passwordValid(connectRequest.userName, connectRequest.password))
            {
                connectResponse = new ClientConnectResponse(serverConnectResponse.invalidCredentials);
            }

            // Check explicitly for username and password match
            else if(db.userExists(connectRequest.userName) && db.passwordValid(connectRequest.userName, connectRequest.password))
            {
                connectResponse = new ClientConnectResponse(serverConnectResponse.success);
                loggedUser = connectRequest.userName; 
            }

            // Unknown issue with login
            else
            {
                // TODO change return code to something legit
                connectResponse = new ClientConnectResponse(serverConnectResponse.serverBusy);
            }

        }
        formatter.Serialize(netStream, connectResponse);

        return loggedUser;
    }
}