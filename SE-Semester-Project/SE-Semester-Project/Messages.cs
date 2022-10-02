using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SE_Semester_Project
{
    public partial class Messages : Form
    {
        public Messages()
        {
            InitializeComponent();
        }

        private void Messages_Load(object sender, EventArgs e)
        {
            cboMessages.Items.Add("test.wav");
            cboMessages.Items.Add("test.txt");
            cboMessages.Items.Add("test.mp4");

            //Hide text box
            txtMessages.Visible = false;
        }

        private void cboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            String selectedMessage = "";
            selectedMessage = cboMessages.SelectedItem.ToString();
            if(selectedMessage.Contains(".wav") || selectedMessage.Contains(".mp4"))
            {
                //Hide text box and display media player
            }
            else
            {
                //Display a text box
            }
            
        }
    }
}
