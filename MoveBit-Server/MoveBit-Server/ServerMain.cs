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
        try
        {
            string name = userLogin(netStream);
            if (name != null)
            {
                user = UserDatabase.GetTheDatabase().getUser(name);
                user.setOnline();
            }
        }
        catch(Exception exception)
        {
            string clientName = (user == null) ? "Unknown User" : user.userName;
            Console.WriteLine($"\tAn exception occured whilst communicating with {clientName}: {exception.ToString()}");
        }
        finally
        {
            if(user != null)
                user.setOffline();

            Console.WriteLine("\tSession with client ended");
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
            connectResponse = new ClientConnectResponse(-1, serverConnectResponse.invalidCredentials);
        }
        // User trying to create a new account
        else if (connectRequest.createAccountFlag)
        {
            // Username taken
            if (db.userExists(connectRequest.userName))
            {
                connectResponse = new ClientConnectResponse(-1, serverConnectResponse.usernameTaken);
            }

            // Insert additional cases here...
            // else if() ...

            // User able to create account
            else if(db.insertUserIfNotTaken(connectRequest.userName, connectRequest.password))
            {
                // Inserted into database
                connectResponse = new ClientConnectResponse(0, serverConnectResponse.success);
                Console.WriteLine($"\tRegistered new user: {connectRequest.userName}");
                loggedUser = connectRequest.userName;
            }
            // Unable to insert... 
            else
            {
                connectResponse = new ClientConnectResponse(-1, serverConnectResponse.usernameTaken);
            }
        }
        // User trying to log into existing account
        else
        {
            // Check if username or password invalid
            if(!db.userExists(connectRequest.userName) || !db.passwordValid(connectRequest.userName, connectRequest.password))
            {
                connectResponse = new ClientConnectResponse(-1, serverConnectResponse.invalidCredentials);
            }

            // Check explicitly for username and password match
            else if(db.userExists(connectRequest.userName) && db.passwordValid(connectRequest.userName, connectRequest.password))
            {
                connectResponse = new ClientConnectResponse(0, serverConnectResponse.success);
                loggedUser = connectRequest.userName; 
            }

            // Unknown issue with login
            else
            {
                // TODO change return code to something legit
                connectResponse = new ClientConnectResponse(-1, serverConnectResponse.serverBusy);
            }

        }
        formatter.Serialize(netStream, connectResponse);

        return loggedUser;
    }
}