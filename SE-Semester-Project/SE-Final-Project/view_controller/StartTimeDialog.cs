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
    public partial class StartTimeDialog : Form
    {

        //Private fields

        private string seconds = "";

        public StartTimeDialog()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            seconds = txtSS.Text + "s";

            this.Hide();
        }

        //Getters and Setters
        public String getSeconds()
        {
            return this.seconds;
        }

        public TextBox getTxtSS()
        {
            return txtSS;
        }

        private void StartTimeDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

       
    }
}
