using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using MoveBitMessaging;

namespace MoveBit_Server
{
    /// <summary>
    /// Class meant to model a user of the system
    /// </summary>
    internal class UserAccount
    {
        private static ServerLogger logger = ServerLogger.GetTheLogger();

        public string userName;                         // User's username (unique)
        public byte[] password;                         // SHA256 password 
        private List<MoveBitMessage> inbox;             // List of messages for the user
        private Object userLock = new Object();         // Lock for accessing user's inbox


        /// <summary>
        /// Constructor for a new UserAccount object
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public UserAccount(string userName, byte[] password)
        {
            this.userName = userName;
            this.password = password;
            inbox = new List<MoveBitMessage>();
        }

        /// <summary>
        /// Adds a new message to a user's inbox
        /// </summary>
        /// <param name="message"></param>
        public void AddMessageToInbox(MoveBitMessage message)
        {
            lock(userLock)
            {
                inbox.Add(message);
                logger.Trace($"Added new message to {userName}'s inbox");
            }
        }


        /// <summary>
        /// Function testing if the user has unread messages.
        /// </summary>
        /// <returns>true if the user has any messages in their server-side inbox</returns>
        public bool HasUnreadMessages()
        {
            return (inbox.Count > 0); 
        }


        /// <summary>
        /// Retrieve the list of unread messages for this user. Calling this function
        /// clears the user's inbox
        /// </summary>
        /// <returns>A list of all the messages in a user's inbox</returns>
        public List<MoveBitMessage> GetUnreadMessages()
        {
            List<MoveBitMessage> messages;
            lock (userLock)
            {
                messages = new List<MoveBitMessage>(inbox);
                inbox.Clear();
            }
            return messages;
        }

        /// <summary>
        /// Returns if the user has any active connections with the sersver
        /// If they do, they are considered online.
        /// </summary>
        /// <returns></returns>
        public bool IsOnline()
        {
            // Get the database
            ServerDatabase db = ServerDatabase.GetTheDatabase();
            // See if the databse contains ainy active sessions or connections for this user
            return (db.GetUserSessionIDs(userName).Count() > 0)
                && db.GetUserConnections(userName).Count() > 0;
        }

        /// <summary>
        /// Attempt to send a given message to all a user's active connections.
        /// If sending to any connection fails, this function returns false.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool TrySend(MoveBitMessage message)
        {
            bool success = false;
            // Get all the users's connections
            List<UserConnection> connections = ServerDatabase.GetTheDatabase().GetUserConnections(userName);
            int connectionNo = 0;
            // Iterate over each connection, sending as we can
            foreach(UserConnection connection in connections)
            {
                connectionNo++;
                // Try to send to this connectoin
                success = connection.TrySendMessage(message);
                if (!success)
                {
                    // NOTE TODO: layers above this must handle failures correctly. This code probably could
                    //      be made more robust
                    logger.Warning($"Sending {userName} message on connection #{connectionNo} failed");
                    return success;
                }
            }
            return success;
        }
    }
}
