using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoveBit_Server
{
    /// <summary>
    /// Class for managing storing and retrieving data concerning users.
    /// This class is implemented as a singleton
    /// </summary>
    internal class UserDatabase
    {

        // TODO: Change from singleton to static object in ServerMain, probably better that way

        // Table of users, where the primary key is the uersername
        Dictionary<string, UserAccount> userTable;
        // Singleton instance of this object
        private static UserDatabase theDataBase = null;
        // Lock for accessing and modifying the database.
        private Object dbLock = new Object();

        /// <summary>
        /// Constructor for the database. Currently set to private sinc this class
        /// is a constructor
        /// </summary>
        private UserDatabase()
        {
            userTable = new Dictionary<string, UserAccount>();

        }

        /// <summary>
        /// Static function for retrieving the singleton database.
        /// </summary>
        /// <returns></returns>
        public static UserDatabase GetTheDatabase()
        {
            if(theDataBase == null)
                theDataBase = new UserDatabase();
            return theDataBase;
        }

        /// <summary>
        /// Function for inserting a new user into the database if the username is currently
        /// not in the databse. Function body is locked to avoid data race
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool insertUserIfNotTaken(string userName, string password)
        {
            lock (dbLock)
            {
                if (!userExists(userName))
                    return addUser(userName, password);
                return false;
            }

        }

        
        /// <summary>
        /// Check if the given userName is already in the database's user table
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool userExists(string userName)
        {
            return userTable.ContainsKey(userName);
        }


        /// <summary>
        /// Check if a given password matches a specific user's password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool passwordValid(string username, string password)
        {
            // TODO: This function obviously isn't finished, passwords should be stored in some kind of hash
            return userTable[username].password == password;
        }


        /// <summary>
        /// Add a new user to the database given a username and password
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if successful</returns>
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


        /// <summary>
        /// Function for getting a user from the database given the user's name.
        /// Does not check that the user exists.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public UserAccount getUser(string userName)
        {
            return userTable[userName];
        }


        /// <summary>
        /// Returns a list of all the users by username that are connected to the server
        /// </summary>
        /// <returns></returns>
        public List<string> getActiveUsers()
        {
            List<string> active = new List<string>();
            foreach(KeyValuePair<string, UserAccount> entry in userTable)
            {
                if(entry.Value.online)
                    active.Add(entry.Key);
            }
            return active;
        }

    }
}
