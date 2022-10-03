using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Drawing.Image;

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
            //Hard coded items for visual/test purposes
            cboMessages.Items.Add("test.wav");
            cboMessages.Items.Add("test.txt");
            cboMessages.Items.Add("test.mp4");

            //Hide text box and picture box
            txtMessages.Visible = false;
            picSound.Visible = false;
        }

        private void cboMessages_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Store the selected combo box item in a string
            String selectedMessage = "";
            selectedMessage = cboMessages.SelectedItem.ToString();

            //Check the file extension type and display the appropriate screen
            if(selectedMessage.Contains(".wav") || selectedMessage.Contains(".mp4"))
            {
                //Hide text box and display media player
                txtMessages.Visible = false;

                //Display sound animation
                picSound.Visible = true;

                //Simple soundplayer test. Path is hardcoded and not attached to the selected file yet
                SoundPlayer sound = new SoundPlayer("C:/Users/ty2ou/Downloads/test.wav");

                //Play sound file
                sound.Play();
     
            }
            else
            {
                //Display a text box
                txtMessages.Visible = true;
            }
            
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //Create a new frmMain object and display it in the current location.
            Main frmMain = new Main();
            frmMain.Show();
            frmMain.Location = this.Location;
            this.Hide();
        }
    }
}
