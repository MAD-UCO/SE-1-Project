using MoveBit_Server;
using System.Net.Sockets;
using MoveBitMessaging;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/// <summary>
/// Main class for the MoveBitServer. The server is meant to coordinate messaging
/// between all the connected clients
/// </summary>
class MoveBitServer
{
    private static object svrLock = new object();           // Lock for accessing/modifying shared server resources
    private static bool runServer = false;                  // Flag for if we should continue server operations
    private static ServerDatabase serverDatabase;           // Local instance of the database
    private static ServerLogger logger;                     // Local instance of the logger
    private static bool kickIdle = false;                   // (experimental/unused) if we should kick idle users
    private static bool loginServiceRunning = false;        // If the login service is/should continue to run
    private static TcpListener server;                      // The TCP listener that clients connect through
    private static int usersToProcess = 0;                  // How many users we have to process
    private static double allocatedUserTime = 1.0;          // How many seconds allowed per user for processing before we report it as an issue
    private static bool plannedListenerShutdown = false;

    /// <summary>
    /// Main execution method for the server
    /// </summary>
    /// <param name="args"></param>
    public static void Main(string[] args)
    {
        logger = ServerLogger.GetTheLogger();
        serverDatabase = ServerDatabase.GetTheDatabase();
        CommandLineReader clim = new CommandLineReader();

        // Parse the command line for any arguments
        if (clim.ParseCommandLine(args))
        {
            clim.EchoSettings();
            kickIdle = clim.kickIdleUsers;
            logger.Important("Starting the MoveBit Server...");

            // Run in localhost mode
            if (clim.acceptOnlyLocal)
            {
                server = new TcpListener(System.Net.IPAddress.Loopback, clim.listeningPort);
                logger.Debug("Server listening on loopback only");
            }

            // Accept any IP connection
            else
            {
                server = new TcpListener(System.Net.IPAddress.Any, clim.listeningPort);
                logger.Debug("Server listening on all interfaces");
            }

            runServer = true;

        }
        else
            logger.Error("The command line arguments could not be set");

        if (runServer)
        {
            try
            {
                server.Start();
                // Send login service to its own thread
                Thread loginThread = new Thread(LoginService);
                loginThread.Start();
                // Begin working to process connected users
                MaintainUserConnections();
            }
            catch (Exception exception)
            {
                logger.Critical($"Fatal exception caused server to terminate: {exception}");
            }
            finally
            {
                logger.Debug("Server preparing for shutdown....");
                ShutdownListener();
                // Stop databse, stop services, prepare for shutdown
                serverDatabase.SaveDataBase();
                runServer = false;

                while (loginServiceRunning)
                {
                    logger.Trace("Server shutting down... Giving 1.5 seconds for different services...");
                    Thread.Sleep(1500);
                }
            }
        }


        logger.Important("Server shut down. Hit <ENTER> To close console");
        Console.Read();
    }

    /// <summary>
    /// Function for processing and handling any users that are
    /// currently connected to the server. This function works continuously
    /// until told to shutdown, monitoring user connections and handling
    /// any requests from users.
    /// </summary>
    public static void MaintainUserConnections()
    {
        logger.Debug($"Beginning the user connection processing function");
        try
        {
            int idleCycles = 0;
            while (runServer)
            {
                List<UserAccount> connectedUsers = serverDatabase.GetConnectedUsers();
                if (connectedUsers.Count == 0)
                {
                    idleCycles++;
                    if (idleCycles % 100 == 0)
                        logger.Info($"Connection processer has been idle for {idleCycles} cycles");
                    Thread.Sleep(200);

                    if (idleCycles >= 150)
                    {
                        logger.Important("TEST - Connection idle for too long - exiting the server");
                        runServer = false;
                        continue;
                    }
                }
                else
                {
                    // Set how many users we must process
                    // NOTE: (see warning message concerning lock below)
                    //  message has printed once, so this method probably isn't great.
                    //  works for quick implementation but we'll need to revisist and make more roboust
                    //  The reason for this is to wait until all work items are completed before we 
                    //  continue with the rest of processing
                    lock (svrLock)
                        usersToProcess = connectedUsers.Count;

                    idleCycles = 0;
                    double timeStart = ((DateTimeOffset)(DateTime.Now)).ToUnixTimeSeconds();

                    // Start a new work item for each user
                    List<UserAccount> connectedCopy = new List<UserAccount>(connectedUsers);
                    foreach (UserAccount userAccount in connectedCopy)
                        ThreadPool.QueueUserWorkItem(ProcessActiveUser, userAccount);

                    double deltaTime = 0.0;
                    bool reported = false;
                    while (usersToProcess > 0)
                    {

#if !THREAD_DEBUG       // directive here so if we are debugging across threads, we don't suddenly have the console print hundreds of these messages

                        // Allow n second(s) of processing time per user connected before we start complaining
                        double diff = ((DateTimeOffset)(DateTime.Now)).ToUnixTimeSeconds() - timeStart;
                        if (diff >= allocatedUserTime * connectedUsers.Count && diff >= deltaTime)
                        {
                            logger.Warning($"User processing is taking exceptionally long ({diff} seconds)");
                            logger.Warning($"{usersToProcess} users remain in processing and are holding up the Processing service");
                            deltaTime = diff + 1.0; // Ensures we don't hammer the reporting
                            reported = true;
                        }

#endif
                    };  // Spin 

                    if (reported)
                        logger.Important($"Previous blockage ended, resuming connection processing service");

                    if (usersToProcess < 0)
                    {
                        logger.Warning($"'usersToProcess' set to {usersToProcess}, implies lock mechanism is not working as intended");
                        usersToProcess = 0;
                    }

                }
                // Now that we have handled all messags, tell the DB to update if it needs to
                serverDatabase.UpdateUserInfo();
            }
        }
        catch (Exception exception)
        {
            runServer = false;
            logger.Critical($"User maintainence loop forced to quit by exception: {exception}");
            throw;
        }
        finally
        {
            logger.Debug("The user connection processing function has terminated");
        }
    }

    /// <summary>
    /// Function begins the login service, which listnes for any connections
    /// and attempts to log in any new connections.
    /// </summary>
    public static void LoginService()
    {
        logger.Trace("Starting the login service");
        TcpClient client;
        loginServiceRunning = true;
        try
        {
            while (runServer)
            {
                // TODO: make async.  Currently this function will hang forever unless 
                // the server adds a new job and runsServer is set to false
                // (or an exception occurs)
                client = server.AcceptTcpClient();
                ThreadPool.QueueUserWorkItem(LoginUser, client);
            }
        }
        catch (SocketException exception)
        {
            if (plannedListenerShutdown)
                logger.Important("Login service being shut down...");
            else
            {
                logger.Critical($"Unplanned server shutdown due to SocketException: {exception}");
                throw;
            }
        }
        catch (Exception exception)
        {
            logger.Critical($"Login service was forced to quit by exception: {exception}");
            throw;
        }
        finally
        {
            loginServiceRunning = false;
        }

        logger.Trace("Login service exiting");
        return;
    }

    /// <summary>
    /// Function for logging in a new user connecting to the server. 
    /// Meant to executed by a worker thread
    /// </summary>
    /// <param name="clientObj"></param>
    public static void LoginUser(Object clientObj)
    {
        logger.Trace("Logging in new user");
        TcpClient client = (TcpClient)clientObj;
        NetworkStream netStream = client.GetStream();
        // Assumes first message will be ClientConnectRequest... May need to make more robust in the future
        ClientConnectRequest connectRequest = (ClientConnectRequest)MessageManager.GetMessageFromStream(netStream);
        ClientConnectResponse connectResponse;

        // User sent one or both fields as empty, dissallow
        if (connectRequest.userName == "" || connectRequest.password == new byte[32])
        {
            logger.Debug($"Connection to new user rejected: failed to pass basic input validation checking");
            connectResponse = new ClientConnectResponse(serverConnectResponse.invalidCredentials);
        }

        // User is creating new account
        else if (connectRequest.createAccountFlag)
        {
            if (serverDatabase.UserExists(connectRequest.userName))
            {
                logger.Debug($"Connection to new user rejected: username already taken");
                connectResponse = new ClientConnectResponse(serverConnectResponse.usernameTaken);
            }

            // Insert additional cases here...
            // else if() ...

            else if (serverDatabase.InsertUserIfNotExist(connectRequest.userName, connectRequest.password))
            {
                // Inserted into database
                logger.Debug($"Connection to new user accepted: {connectRequest.userName} (new user) has been allowed to connect");
                byte[] sessionID = serverDatabase.GenerateAndAddUserSession(connectRequest.userName, client);
                connectResponse = new ClientConnectResponse(serverConnectResponse.success, sessionID);
            }

            else
            {
                logger.Warning($"Connection to new user rejected for unknown reason!");
                connectResponse = new ClientConnectResponse(serverConnectResponse.unknownError);
            }
        }

        // User trying to log into existing account
        else
        {

            bool letUserLogin = serverDatabase.UserExists(connectRequest.userName)
                && serverDatabase.UserPasswordIsValid(connectRequest.userName, connectRequest.password);

            // Check if username or password invalid
            if (!letUserLogin)
            {
                logger.Debug($"Connection to new user rejected: They did not exist in DB or had invalid credentials");
                connectResponse = new ClientConnectResponse(serverConnectResponse.invalidCredentials);
            }

            else if (letUserLogin)
            {
                // TODO: Send session ID back
                logger.Debug($"Connection to new user accepted: {connectRequest.userName} has been allowed to connect");
                byte[] sessionID = serverDatabase.GenerateAndAddUserSession(connectRequest.userName, client);
                connectResponse = new ClientConnectResponse(serverConnectResponse.success);
            }

            else
            {
                logger.Warning($"Connection to new user rejected for unknown reason!");
                connectResponse = new ClientConnectResponse(serverConnectResponse.unknownError);
            }
        }

        MessageManager.WriteMessageToNetStream(connectResponse, netStream);
        logger.Trace("Finished with user login");
    }

    /// <summary>
    /// Function for processing a connected user
    /// Meant to be executed from a worker thread
    /// </summary>
    /// <param name="userObj"></param>
    public static void ProcessActiveUser(Object userObj)
    {

        UserAccount user = (UserAccount)userObj;
        logger.Trace($"Starting new processing service for {user.userName}");
        if (user.IsOnline())
        {
            List<MoveBitMessage> messages = CheckForNewMessagesFromUser(user);  // Get incoming
            messages = ProcessIncomingUserMessages(messages, user);             // Process incoming
            ProcessOutgoingMessages(messages, user);                            // Process outgoing
        }
        else
            logger.Trace($"{user.userName} is no longer online");

        // Decrement how many users we have left to process
        lock (svrLock)
            usersToProcess--;
    }

    /// <summary>
    /// Returns a list of messages the user has sent us (if any)
    /// Checks each connection with a user is checked
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    public static List<MoveBitMessage> CheckForNewMessagesFromUser(UserAccount user)
    {
        List<MoveBitMessage> incomingMessages = new List<MoveBitMessage>();
        // Get and iterate over each connection individually
        List<UserConnection> userConnections = serverDatabase.GetUserConnections(user.userName);
        foreach (UserConnection userConnection in userConnections)
        {
            // While there is more to read...
            while (userConnection.DataReady())
            {
                incomingMessages.Add(userConnection.GetMessage());
                logger.Debug($"New message recieved from {user.userName}");
            }
        }

        return incomingMessages;
    }

    /// <summary>
    /// Processing incoming messages given a user account and a list of messages to send.
    /// Returns messages we need to send the user back, if any
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public static List<MoveBitMessage> ProcessIncomingUserMessages(List<MoveBitMessage> messages, UserAccount user)
    {
        List<MoveBitMessage> serverMessages = new List<MoveBitMessage>();

        foreach (MoveBitMessage message in messages)
        {
            // Add new messages here as needed
            if (message.GetType() == typeof(SimpleTextMessage))
            {
                SimpleTextMessageResult result;
                SimpleTextMessage msg = (SimpleTextMessage)message;
                if (serverDatabase.UserExists(msg.recipient))
                {
                    UserAccount targetUser = serverDatabase.GetUser(msg.recipient);
                    if (targetUser != null)
                    {
                        result = new SimpleTextMessageResult(SendResult.sendSuccess);
                        targetUser.AddMessageToInbox(msg);
                    }
                    else
                        result = new SimpleTextMessageResult(SendResult.sendFailure);

                }
                else
                    result = new SimpleTextMessageResult(SendResult.sendFailure);
                serverMessages.Add(result);
            }
            else if (message.GetType() == typeof(TestListActiveUsersRequest))
            {
                serverMessages.Add(new TestListActiveUsersResponse(serverDatabase.GenerateOnlineUserReport()));
            }
            else
            {
                logger.Error($"Recieved an unknown type of message from {user.userName}!");
            }
        }

        return serverMessages;
    }

    /// <summary>
    /// Sends a given list of messages the given user. 
    /// </summary>
    /// <param name="messages"></param>
    /// <param name="user"></param>
    public static void ProcessOutgoingMessages(List<MoveBitMessage> messages, UserAccount user)
    {
        // If the user is online, send them their unread messages too
        if (user.IsOnline())
            messages.AddRange(user.GetUnreadMessages());

        // If they are not online or we don't have anything to send them, exit
        if (!user.IsOnline() || messages.Count == 0)
            return;



        logger.Debug($"Server sending {messages.Count} messages to {user.userName}");

        foreach (MoveBitMessage message in messages)
        {
            if (user.IsOnline() && user.TrySend(message))
                ; // If user is online and trySend succeeds, do nothing...
            else
                user.AddMessageToInbox(message); // Otherwise, add to their inbox for later
        }
    }

    private static void ShutdownListener()
    {
        plannedListenerShutdown = true;
        server.Stop();
    }
}