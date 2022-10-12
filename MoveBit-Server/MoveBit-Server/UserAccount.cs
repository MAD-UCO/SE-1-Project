using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MoveBitMessaging;

namespace MoveBit_Server
{
    /// <summary>
    /// Class meant to model a user of the system
    /// </summary>
    internal class UserAccount
    {

        public string userName;
        public string password;
        public bool online;
        public bool isBanned = false;
        // List of messages for the user
        private List<MoveBitMessage> inbox;
        public TcpClient client;
        public NetworkStream clientNetStream;
        // Lock for accessing user's ibox
        private Object userLock = new Object();


        /// <summary>
        /// Constructor for a new UserAccount object
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        public UserAccount(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            online = false;
            inbox = new List<MoveBitMessage>();
        }

        /// <summary>
        /// Sets a user's status to online
        /// </summary>
        public void setOnline()
        {
            online = true;
            //this.client = client;
            //clientNetStream = client.GetStream();
        }
        

        /// <summary>
        /// Sets a user's status to offline
        /// </summary>
        public void setOffline()
        {
            online = false;
        }


        /// <summary>
        /// Adds a new message to a user's inbox
        /// </summary>
        /// <param name="message"></param>
        public void addMessageToInbox(MoveBitMessage message)
        {
            lock(userLock)
            {
                inbox.Add(message);
            }
        }


        /// <summary>
        /// Function testing if the user has unread messages.
        /// </summary>
        /// <returns>true if the user has any messages in their server-side inbox</returns>
        public bool hasUnreadMessages()
        {
            return (inbox.Count > 0); 
        }


        /// <summary>
        /// Retrieve the list of unread messages for this user. Calling this function
        /// clears the user's inbox
        /// </summary>
        /// <returns>A list of all the messages in a user's inbox</returns>
        public List<MoveBitMessage> getUnreadMessages()
        {
            List<MoveBitMessage> messages;
            lock (userLock)
            {
                messages = new List<MoveBitMessage>(inbox);
                inbox.Clear();
            }
            return messages;
        }
    }
}
