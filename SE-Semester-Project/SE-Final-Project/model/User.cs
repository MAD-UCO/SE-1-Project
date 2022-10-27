using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE_Final_Project.model
{
    public class User
    {

        //private fields
        private string userName;
        private string password;
        private string contactAddress;


        //Constructors

        public User()
        {
            userName = "John";
            password = "Doe";
            contactAddress = "100.100.100";
        }

        public User(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            contactAddress = "100.100.100";
        }

        public User(string userName, string password, string contactAddress)
        {
            this.userName = userName;
            this.password = password;
            this.contactAddress = contactAddress;
        }

        //Getters and Setters
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


        public string getContactAddress()
        {
            return this.contactAddress;
        }

        public void setContactAddress(string contactAddress)
        {
            this.contactAddress = contactAddress;
        }

    }
}
