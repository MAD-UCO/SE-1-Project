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
using SE_Final_Project.view_controller;
using SE_Semester_Project;

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
            this.FormClosing += login_FormClosing;
            InitializeComponent();
        }

        //Event handlers
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text == "" || txtUsername.Text == "")
            {
                MessageBox.Show("Username or Password not entered");
            }
            else
            {
                //Create a user instance
                user = new User(txtUsername.Text, txtPassword.Text, chkNewUser.Checked);

                this.Hide();
                Main frmMain = new Main();
                frmMain.ShowDialog();
            }
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

        private void login_FormClosing(object sender, FormClosingEventArgs closingArgs)
        {
            NetworkClient.Logout();
            NetworkClient.Shutdown();
        }

        //Getters
        public User getUser()
        {
            return user;
        }
    }
}
