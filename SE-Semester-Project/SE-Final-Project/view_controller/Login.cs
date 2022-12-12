using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using SE_Final_Project.model;
using SE_Semester_Project;

namespace SE_Final_Project
{
    public partial class Login : Form
    {
        //private variables
        private string username = "";
        private string password = "";
        private User user;
        private Main main;
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        int ticker = 0;
        Splash splash;

        //constants
        private const int HeightAdjustment = 25;

        //Store successful and unsuccessful attempts
        List<bool> attempts = new List<bool>();

        //Class construcctor, do not edit. Use form load event for initialization
        public Login()
        {
            Thread t = new Thread(new ThreadStart(StartForm));
            t.Start();
            Thread.Sleep(3000);
            InitializeComponent();
            t.Abort();
            this.Focus();
        }

        public void StartForm()
        {
            Application.Run(new Splash());
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
                    //Create new main form object
                    main = new Main();

                    //Set location to center of user screen (adjusted up by 25)
                    main.Location = new Point((Screen.PrimaryScreen.Bounds.Size.Width / 2) - (main.Size.Width / 2),
                        (Screen.PrimaryScreen.Bounds.Size.Height / 2) - (main.Size.Height / 2) - 25);
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

        //Return the user
        public User GetUser()
        {
            return user;
        }

        //Private Operations

        //Shut down all processes when user exits program
        private void Login_FormClosing(object sender, FormClosingEventArgs closingArgs)
        {
            NetworkClient.Shutdown();
            NetworkClient.Logout();
            Environment.Exit(0);
        }
    }
}
