using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    internal class UserDatabase
    {
        Dictionary<string, UserAccount> userTable;
        private static UserDatabase theDataBase = null;
        private Object dbLock = new Object();
        private UserDatabase()
        {
            userTable = new Dictionary<string, UserAccount>();

        }

        public static UserDatabase GetTheDatabase()
        {
            if(theDataBase == null)
                theDataBase = new UserDatabase();
            return theDataBase;
        }

        public bool insertUserIfNotTaken(string userName, string password)
        {
            lock (dbLock)
            {
                if (!userExists(userName))
                    return addUser(userName, password);
                return false;
            }

        }
        public bool userExists(string userName)
        {
            return userTable.ContainsKey(userName);
        }

        public bool passwordValid(string username, string password)
        {
            return userTable[username].password == password;
        }

        public bool addUser(string username, string password)
        {
            UserAccount user = new UserAccount(username, password);
            userTable.Add(username, user);
            return true;
        }

        // TODO
        // Eventually we want to have this function actually load information from a database or file
        // For now, it'll just hard code values...
        public void loadDatabase()
        {
            Dictionary<string, string> temp = new Dictionary<string, string>();
            temp.Add("Cheeseballs42", "Ilovecheese");
            temp.Add("admin", "password");

            foreach (KeyValuePair<string, string> entry in temp)
                userTable.Add(entry.Key, new UserAccount(entry.Key, entry.Value));
        }

        public UserAccount getUser(string userName)
        {
            return userTable[userName];
        }

    }
}
