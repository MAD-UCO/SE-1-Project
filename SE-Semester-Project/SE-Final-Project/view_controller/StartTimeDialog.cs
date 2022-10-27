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
        private string hours;
        private string minutes;
        private string seconds;
        private string timeStamp;

        public StartTimeDialog()
        {
            InitializeComponent();
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            seconds = txtSS.Text;
            /*
             * 
             * 
             * Pass the timestamp to message object here
             * 
             */

            this.Hide();
        }
    }
}
