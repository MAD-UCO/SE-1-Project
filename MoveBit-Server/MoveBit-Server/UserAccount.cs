using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoveBitMessaging;

namespace MoveBit_Server
{
    internal class UserAccount
    {
        public string userName;
        public string password;
        public bool online;
        private List<Message> inbox;
        private Object userLock = new Object();

        public UserAccount(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            online = false;
            inbox = new List<Message>();
        }

        public void setOnline()
        {
            online = true;
            Console.WriteLine($"\t{userName} logged in");
            Thread.Sleep(1000);
        }

        public void setOffline()
        {
            online = false;
            Console.WriteLine($"\t{userName} logged out");
            Thread.Sleep(1000);
        }

        public void addMessageToInbox(Message message)
        {
            lock(userLock)
            {
                inbox.Add(message);
            }
        }

        public bool hasUnreadMessages()
        {
            return (inbox.Count > 0); 
        }

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
