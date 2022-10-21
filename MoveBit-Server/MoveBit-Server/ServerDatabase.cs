using MoveBitMessaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    /// <summary>
    /// Class for managing storing and retrieving data concerning users.
    /// This class is implemented as a singleton
    /// </summary>
    internal class ServerDatabase
    {
        private Dictionary<string, UserAccount> userTable;                                  // Table of users, where the primary key is the username
        private Dictionary<string, Dictionary<byte[], UserSession>> sessionTable;           // Table of active sessions, where primary key is username and session id, username references userTable
        private Dictionary<string, Dictionary<byte[], UserConnection>> connectionTable;     // Table of active connections, where primary key is username and session id, username references userTable
        private List<UserAccount> activeUsers;                                              // List of active users according to what we know in the DB
        private static ServerDatabase theDataBase = null;                                   // Singleton instance of this object
        private Object dbLock = new Object();                                               // Lock for accessing and modifying the database.
        public bool timeoutUserSessions = false;                                            // (Experimental/Unused) if we are to kick users who are idle too long

        private ServerLogger logger = ServerLogger.GetTheLogger();

        /// <summary>
        /// Constructor for the database. Currently set to private sinc this class
        /// is a constructor
        /// </summary>
        private ServerDatabase()
        {
            userTable = new Dictionary<string, UserAccount>();
            sessionTable = new Dictionary<string, Dictionary<byte[], UserSession>>();
            connectionTable = new Dictionary<string, Dictionary<byte[], UserConnection>>();
            activeUsers = new List<UserAccount>();
            LoadDataBase();
        }

        /// <summary>
        /// Static function for retrieving the singleton database.
        /// </summary>
        /// <returns></returns>
        public static ServerDatabase GetTheDatabase()
        {
            if (theDataBase == null)
                theDataBase = new ServerDatabase();
            return theDataBase;
        }

        /// <summary>
        /// Returns the list of active users
        /// </summary>
        /// <returns></returns>
        public List<UserAccount> GetConnectedUsers()
        {
            return activeUsers;
        }

        /// <summary>
        /// Generates a unique session ID for a user and creates a new user session
        /// object in the session table. The generated session ID is returned.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public byte[] GenerateAndAddUserSession(string userName, TcpClient client)
        {
            byte[] sessionID = GenerateUniqueSessionID(userName);
            AddUserSession(userName, sessionID, client);
            return sessionID;
        }

        /// <summary>
        /// Generates a unique session ID for a specific user.
        /// Returns the generated session ID
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public byte[] GenerateUniqueSessionID(string userName)
        {
            // Session IDs only need to be unique across a user's list
            // Two users may have the same session ID, but the server can
            // still differientiate them due to usernames being unique
            byte[] id = null;
            do
            {
                id = GenerateSessionID();
            }
            while (SessionValid(userName, id));

            return id;
        }

        /// <summary>
        /// Adds a new users session to the session table and a new
        /// connection to the connection table for a given user.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="sessionId"></param>
        /// <param name="client"></param>
        public void AddUserSession(string userName, byte[] sessionId, TcpClient client)
        {
            UserSession newSession = new UserSession(userName, sessionId, DateTime.Now);
            Dictionary<byte[], UserSession> userSessions = sessionTable[userName];
            userSessions[sessionId] = newSession;

            UserAccount thisUser = userTable[userName];
            if (!activeUsers.Contains(thisUser))
            {
                logger.Info($"{userName} logged in");
                lock (dbLock)
                    activeUsers.Add(thisUser);
            }
            else
                logger.Info($"{userName} started another session");

            UserConnection userStream = new UserConnection(sessionId, client);
            connectionTable[userName][sessionId] = userStream;
        }


        /// <summary>
        /// Returns true if the given username and sessionId in the session table
        /// matches an entry in the database.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="sessionID"></param>
        /// <returns></returns>
        public bool SessionValid(string userName, byte[] sessionID)
        {
            // Get all seessions we have with this user
            List<byte[]> userSessions = GetUserSessionIDs(userName);
            // return if our ID is one of them
            return userSessions.Contains(sessionID);
        }

        /// <summary>
        /// Retrieves all the session Ids for a given user
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<byte[]> GetUserSessionIDs(string userName)
        {
            Dictionary<byte[], UserSession> userSessions;
            List<byte[]> sessionList = new List<byte[]>();
            if (UserExists(userName))
            {
                if (sessionTable.ContainsKey(userName))
                {
                    userSessions = sessionTable[userName];
                    sessionList = new List<byte[]>(userSessions.Keys);
                }
            }
            else
                logger.Warning($"No user named {userName} exists in the database and thus cannot have any sessions");

            return sessionList;
        }

        /// <summary>
        /// Tries to look up a user by username.
        /// Returns if the username is in our user table
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool UserExists(string userName)
        {
            return userTable.ContainsKey(userName);
        }

        /// <summary>
        /// Function for trying to insert a new user into the user table
        /// Handles the potential asynchronous accesses into the database.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool InsertUserIfNotExist(string username, byte[] password)
        {
            bool success = false;
            // Lock the database so no one else may insert
            lock (dbLock)
            {
                // One last check to make sure we may insert the 
                // user
                if (!UserExists(username))
                {
                    InsertUser(username, password);
                    success = true;
                }
            }
            return success;
        }

        /// <summary>
        /// Returns if a user's password hash equals what we have.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserPasswordIsValid(string userName, byte[] password)
        {
            return userTable[userName].password.SequenceEqual(password);
        }

        /// <summary>
        /// Inserts a user into the database. This function does not operate using locks
        /// and does not test if the user is already in the database, and should only be
        /// used by the database through 'InsertUserIfNotExist'
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        private void InsertUser(string userName, byte[] password)
        {
            UserAccount newUser = new UserAccount(userName, password);
            userTable[userName] = newUser;
            sessionTable.Add(userName, new Dictionary<byte[], UserSession>());
            connectionTable.Add(userName, new Dictionary<byte[], UserConnection>());
        }

        /// <summary>
        /// Function for generating a random MD5 session ID
        /// </summary>
        /// <returns></returns>
        private byte[] GenerateSessionID()
        {
            // Nothing about this function is specific to the DB
            // Could move out if we want
            byte[] id = new byte[32];
            using (MD5 md5 = MD5.Create())
            {
                Random random = new Random();
                int rng = random.Next(1, Int32.MaxValue);
                id = md5.ComputeHash(BitConverter.GetBytes(rng));
            }
            return id;
        }

        /// <summary>
        /// Loads the database with initial information
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void LoadDataBase()
        {
            // TODO: pass a file path so we may load local files
            logger.Debug("Loading database");
#if DEBUG
            logger.Debug("Loading database with dummy data");
            bool successful = true;
            successful &= InsertUserIfNotExist("admin", SHA256HashShortcut("password"));

            if (!successful)
                throw new Exception("The server could not initialize the test accounts!");
#else
            throw new NotImplementedException("This functionality has not been completed for the release build!");
#endif
        }

        /// <summary>
        /// Function for saving the database
        /// </summary>
        public void SaveDataBase()
        {
            // TODO
        }

        /// <summary>
        /// Gets a list of a given user's active connection objects
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public List<UserConnection> GetUserConnections(string userName)
        {

            List<UserConnection> userConnections = new List<UserConnection>();

            if (UserExists(userName))
            {
                foreach (byte[] userSession in GetUserSessionIDs(userName))
                    userConnections.Add(connectionTable[userName][userSession]);
            }
            else
                logger.Warning($"No user with username {userName} exists in database and thus cannot have any active connections");

            return userConnections;
        }

        /// <summary>
        /// Checks all users connections. If any connection is found to be dead,
        /// end the connection and the session. Remove each from their respective
        /// tables.
        /// </summary>
        /// <param name="user"></param>
        private void UpdateActiveUserConnections(UserAccount user)
        {
            List<UserConnection> userConnections = GetUserConnections(user.userName);

            foreach (UserConnection connection in userConnections)
            {
                if (!connection.IsActive())
                {
                    logger.Info($"A session with {user.userName} was ended");
                    EndUserSession(user, connection.sessionID);
                }
            }
        }

        /// <summary>
        /// Ends a given user's session by ID
        /// </summary>
        /// <param name="user"></param>
        /// <param name="sessionID"></param>
        private void EndUserSession(UserAccount user, byte[] sessionID)
        {
            if (sessionTable[user.userName].ContainsKey(sessionID))
            {
                sessionTable[user.userName].Remove(sessionID);
                logger.Trace($"Session with {user.userName} successfully ended");
            }
            else
                logger.Warning($"Request to end session for {user.userName} failed, as the session did not exist!");

            if (connectionTable[user.userName].ContainsKey(sessionID))
            {
                connectionTable[user.userName][sessionID].End();
                connectionTable[user.userName].Remove(sessionID);
                logger.Trace($"Connection with {user.userName} successfully closed");
            }
            else
                logger.Warning($"Reques to end user connection for {user.userName} by session failed, as the session did not exist!");

            // If that was the last active session with the user
            // remove them from the list of those online
            if (!user.IsOnline())
            {
                activeUsers.Remove(user);
                logger.Info($"{user.userName} is no longer online");
            }
        }

        /// <summary>
        /// Server maintains itself by checking each user connection and 
        /// updating the tables appropriately if a user's connection changes
        /// </summary>
        public void UpdateUserInfo()
        {
            logger.Trace("Updating user info");
            lock (dbLock)
            {
                List<UserAccount> activeCopy = new List<UserAccount>(activeUsers);
                foreach (UserAccount user in activeCopy)
                {
                    UpdateActiveUserConnections(user);
                }

            }

        }

        /// <summary>
        /// Returns a specific user's UserAccount object given their username
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserAccount GetUser(string userName)
        {
            UserAccount user = null;
            if (UserExists(userName))
                user = userTable[userName];

            return user;

        }

        /// <summary>
        /// Function for listing all users who are registered on the server and if they are connected
        /// Mostly as a proof of concept / testing
        /// </summary>
        /// <returns></returns>
        public List<string> GenerateOnlineUserReport()
        {
            List<string> report = new List<string>();
            foreach (UserAccount user in userTable.Values)
                report.Add(user.userName + (user.IsOnline() ? " (Online)" : " (Offline)"));

            return report;
        }

#if DEBUG
        /// <summary>
        /// Shortcut for ust to hash the passwords in the hard-coded
        /// section of the LoadDatabase function
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private byte[] SHA256HashShortcut(string password, string salt = "")
        {
            byte[] hash;
            using (SHA256 sha = SHA256.Create())
            {
                password += salt;

                hash = sha.ComputeHash(Encoding.ASCII.GetBytes(password));
            }

            return hash;
        }
#endif
    }
}
