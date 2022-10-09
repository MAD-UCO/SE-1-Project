using System;
using System.Collections.Generic;
using System.Linq;
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
        // List of messages for the user
        private List<Message> inbox;
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
            inbox = new List<Message>();
        }

        /// <summary>
        /// Sets a user's status to online
        /// </summary>
        public void setOnline()
        {
            online = true;
            // Todo remove below here
            Console.WriteLine($"\t{userName} logged in");
            Thread.Sleep(1000);
        }
        

        /// <summary>
        /// Sets a user's status to offline
        /// </summary>
        public void setOffline()
        {
            online = false;
            // Todo remove below here
            Console.WriteLine($"\t{userName} logged out");
            Thread.Sleep(1000);
        }


        /// <summary>
        /// Adds a new message to a user's inbox
        /// </summary>
        /// <param name="message"></param>
        public void addMessageToInbox(Message message)
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
        public List<Message> getUnreadMessages()
        {
            List<Message> messages;
            lock (userLock)
            {
                messages = new List<Message>(inbox);
                inbox.Clear();
            }
            return messages;
        }
    }
}
