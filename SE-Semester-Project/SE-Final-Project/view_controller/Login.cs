using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SE_Final_Project.model;

namespace SE_Final_Project
{
    public partial class Login : Form
    {

        private string username = "";
        private string password = "";
        private User user;

        //Store successful and unsuccessful attempts
        List<bool> attempts = new List<bool>();

        //Class construcctor, do not edit. Use form load event for initialization
        public Login()
        {
            InitializeComponent();
        }

        //Runs when form loads
        private void Login_Load(object sender, EventArgs e)
        {
            //Create a new user instance
            user = new User(txtUsername.Text, txtPassword.Text);
        }

        //Event handlers
        private void btnLogin_Click(object sender, EventArgs e)
        {
            /*
             * 
             * Pass validation info to server here
             * 
             */

            Main frmMain = new Main();
            frmMain.ShowDialog();
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            //Save the input text from txtUsername
            username = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            //Save the input text from txtPassword
            password = txtPassword.Text;
        }

        //Getters
        public User getUser()
        {
            return user;
        }
    }
}
