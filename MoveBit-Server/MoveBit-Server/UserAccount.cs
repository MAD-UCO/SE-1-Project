using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    internal class UserAccount
    {
        public string userName;
        public string password;
        public bool online;
        private List<string> inbox;

        public UserAccount(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            online = false;
            inbox = new List<string>();
        }

        public void setOnline()
        {
            online = true;
            Console.WriteLine($"\t{userName} logged in");
            Thread.Sleep(10000);
        }

        public void setOffline()
        {
            online = false;
            Console.WriteLine($"\t{userName} logged out");
            Thread.Sleep(10000);
        }

        public void addMessageToInbox(string message)
        {
            inbox.Add(message);
        }
    }
}
