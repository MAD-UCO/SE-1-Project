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

namespace SE_Final_Project
{
    public partial class Login : Form
    {

        private string username = "";
        private string password = "";

        //Store successful and unsuccessful attempts
        List<bool> attempts = new List<bool>();

        //Class construcctor, do not edit. Use form load event for initialization
        public Login()
        {
            InitializeComponent();
        }

        //Event handlers
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Main frmMain = new Main();
            frmMain.ShowDialog();
            this.Hide();
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
        public string getUsername()
        {
            return username;
        }

        public string getPassword()
        {
            return password;
        }

        public List<bool> getAttempts()
        {
            return attempts;
        }
    }
}
