using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Final_Project
{
    public partial class Duration : Form
    {
        //Private fields
        private string seconds = "";

        //Class constructor, do not edit. Use form load event for initialization.
        public Duration()
        {
            InitializeComponent();
        }

        //Event Handlers
        private void btnAccept_Click(object sender, EventArgs e)
        {
            seconds = txtSS.Text + "s";

            this.Hide();
        }

        //Getters
       
        public string getSeconds()
        {
            return seconds;
        }

        private void Duration_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
