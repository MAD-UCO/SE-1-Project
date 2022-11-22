using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Final_Project.model
{
    internal class Event
    {
        enum State
        {
            newUserLogin,
            existingUserLogin
        }

        private bool isNewUser;
        private string userName;
        private string password;
        private Event loginStatus;

        public Event(bool isNewUser, string userName, string passWord)
        {
            this.isNewUser = isNewUser;
            this.userName = userName;
            this.password = passWord;
        }

        //Getters/Setters
        public bool getIsNewUser()
        {
            return this.isNewUser;
        }

        public void setIsNewUser(bool isNewUser)
        {
            this.isNewUser = isNewUser;
        }

        public string getUserName()
        {
            return this.userName;
        }

        public void setUserName(string userName)
        {
            this.userName = userName;
        }

        public string getPassword()
        {
            return this.password;
        }

        public void setPassword(string password)
        {
            this.password = password;
        }

        public Event getLoginStatus()
        {
            return loginStatus;
        }
    }
}
