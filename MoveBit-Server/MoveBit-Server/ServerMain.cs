using MoveBit_Server;
using System.Runtime.Serialization;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using MoveBitMessaging;
using System.Diagnostics.Tracing;

/// <summary>
/// Main class for the MoveBitServer. The server is meant to coordinate messaging
/// between all the connected clients
/// </summary>
class MoveBitServer
{
    /// <summary>
    /// Main execution function
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        // Load local databases
        UserDatabase userDatabase = UserDatabase.GetTheDatabase();
        userDatabase.loadDatabase();

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

            // Start up the server
            server.Start();
            try
            {
                // TODO: 'runServer' only set to false when exception reaches this high... May want to make some kind of disconnect
                //      message in order to control the shutdown.
                while (runServer)
                {
                    // Accept a new client and relegate processing to a new thread
                    client = server.AcceptTcpClient();
                    ThreadPool.QueueUserWorkItem(processUser, client);
                }
            }
            // Something went horribly wrong...
            catch(Exception exception)
            {
                Console.WriteLine($"An error occured while running the server: {exception.Message}");
                runServer = false;
                // TODO: try to save databases, safely disconnect users, and other last minute things...
            }

        }
        // Something went wrong while parsing the command line...
        else
            Console.WriteLine("The command line arguments could not be set");

        // TODO: If the server crashes, send all the clients a message telling them to disconnect so
        //      they are in a safe state
        Console.WriteLine("Shutting down the server...");
    }

    /// <summary>
    /// Function for handling a user. The lifetime of this function is bound
    /// to that of a user's session
    /// </summary>
    /// <param name="clientObj"></param>
    public static void processUser(Object clientObj)
    {
        Console.WriteLine($"\tNew client connected");
        TcpClient client = (TcpClient)clientObj;
        // TODO: Redundant netstreams and formatters... Get rid of & clean up
        NetworkStream netStream = client.GetStream();
        UserAccount user = null;
        BinaryFormatter formatter = new BinaryFormatter();
        try
        {
            // Get users name from login function
            string name = userLogin(netStream);
            // TODO: If name is not null user should be in the database...
            //      However, we should still check... Fix this.
            if (name != null)
            {
                // Get the userAccount object tied to the username
                user = UserDatabase.GetTheDatabase().getUser(name);
                user.setOnline();

                bool endConnetion = false;
                while (!endConnetion)
                {
                    // Don't read from the stream unless there is actually something there...
                    if (netStream.DataAvailable)
                    {
                        // We got a new message... Read it.
                        // TODO: change from single character variable
                        Message m = MessageOperator.netStreamToMessage(netStream);
                        // User wants a list of all users.
                        if (m.GetType() == typeof(TestListActiveUsersRequest))
                        {
                            List<string> activeUsers;
                            // Create & send response for active users
                            TestListActiveUsersResponse response = new TestListActiveUsersResponse(UserDatabase.GetTheDatabase().getActiveUsers());
                            formatter.Serialize(netStream, response);
                        }

                        // User wants to send a text messsage
                        else if(m.GetType() == typeof(SimpleTextMessage))
                        {
                            SimpleTextMessage message = (SimpleTextMessage)m;
                            SimpleTextMessageResult result;
                            UserDatabase userDatabase = UserDatabase.GetTheDatabase();
                            // Target user does in fact exist
                            if (userDatabase.userExists(message.recipient))
                            {
                                // Get user account and add to their inbox
                                UserAccount userAccount = userDatabase.getUser(message.recipient);
                                userAccount.addMessageToInbox(message);
                                // Tell user we sent the message
                                result = new SimpleTextMessageResult(SendResult.sendSuccess);
                            }

                            // Target user couldn't be found... Tell them something went wrong
                            else
                                result = new SimpleTextMessageResult(SendResult.sendFailure);

                            // Send the message
                            formatter.Serialize(netStream, result);
                        }
                    }
                    // Test if client port has been abandoned...
                    else if (client.Client.Poll(1000, SelectMode.SelectRead)) 
                    {
                        endConnetion = true;
                        Console.WriteLine($"\t{user.userName} disconnected");
                        netStream.Close();
                    }
                    // User has unread messages and we can tell them!
                    else if (user.hasUnreadMessages() && netStream.CanWrite)
                    {
                        List<Message> messages = user.getUnreadMessages();
                        Console.Write($"\tSending {user.userName} their {messages.Count()} new messages");
                        InboxListUpdate update = new InboxListUpdate(messages);
                        formatter.Serialize(netStream, update);
                    }

                    // TODO make 'else'
                    // Nothing to do, sleep for a bit and wait for something to happen
                    Thread.Sleep(250);
                }
            }
        }
        // Something went wrong while handling the user...
        catch(Exception exception)
        {
            string clientName = (user == null) ? "Unknown User" : user.userName;
            Console.WriteLine($"\tAn exception occured whilst communicating with {clientName}: {exception.ToString()}");
        }
        finally
        {
            // Tidy before we leave
            if (user != null)
                user.setOffline();
            client.Close();
        }

    }

    /// <summary>
    /// Function for handling the initial step of ensureing a valid user is logging in
    /// </summary>
    /// <param name="netStream"></param>
    /// <returns></returns>
    public static string userLogin(NetworkStream netStream)
    {
        string loggedUser = null;
        // TODO: *MORE* redundant formatters! Get rid of these!
        BinaryFormatter formatter = new BinaryFormatter();
        ClientConnectRequest connectRequest = (ClientConnectRequest)formatter.Deserialize(netStream);
        ClientConnectResponse connectResponse;

        UserDatabase db = UserDatabase.GetTheDatabase();
        // TODO: Probably make a general 'validInput' function for the connectionRequest
        // User didn't provide a username or password...
        if (connectRequest.userName == "" || connectRequest.password == "")
        {
            connectResponse = new ClientConnectResponse(serverConnectResponse.invalidCredentials);
        }
        // User trying to create a new account
        else if (connectRequest.createAccountFlag)
        {
            // TODO Delete some of these unnecessary braces.

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
        // Send the connect response message
        formatter.Serialize(netStream, connectResponse);

        // Return the user name, if we got it
        return loggedUser;
    }
}