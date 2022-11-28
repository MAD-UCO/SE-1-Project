using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SE_Final_Project.model;
using SE_Semester_Project;

namespace SE_Final_Project
{
    public partial class Login : Form
    {

        private string username = "";
        private string password = "";
        private User user;
        private Main main;

        //Store successful and unsuccessful attempts
        List<bool> attempts = new List<bool>();

        //Class construcctor, do not edit. Use form load event for initialization
        public Login()
        {
            InitializeComponent();
        }

        //Event handlers
        private void BtnLogin_Click(object sender, EventArgs e)
        {
            if(txtPassword.Text == "" || txtUsername.Text == "")
            {
                MessageBox.Show("Username or Password not entered");
            }
            else if(NetworkClient.Login(txtUsername.Text, txtPassword.Text, chkNewUser.Checked))
            {
                //Create a user instance
                user = new User(txtUsername.Text, txtPassword.Text, chkNewUser.Checked);

                this.Hide();
                if(main == null)
                {
                    main = new Main();
                    main.Show();
                }
                else
                {
                    main = (Main)Application.OpenForms["Main"];
                }
                main.Show();
                txtUsername.Clear();
                txtPassword.Clear();
                chkNewUser.Checked = false;
            }
        }

        private void TxtUsername_TextChanged(object sender, EventArgs e)
        {
            //Save the input text from txtUsername
            username = txtUsername.Text;
        }

        private void TxtPassword_TextChanged(object sender, EventArgs e)
        {
            //Save the input text from txtPassword
            password = txtPassword.Text;
        }

        //Getters
        public User GetUser()
        {
            return user;
        }

        //Shut down all processes when user exits program
        private void Login_FormClosing(object sender, FormClosingEventArgs closingArgs)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Application.Exit();
        }
    }
}
