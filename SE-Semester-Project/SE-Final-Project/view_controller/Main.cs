using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

using SE_Semester_Project;

namespace SE_Final_Project
{
    public partial class Main : Form
    {
        
        //private fields
        private string selectedAddress;
        private string selectedFile;
        private List<String> outgoingFilepaths = new List<String>();

        private SE_Semester_Project.Message message;
        private TextMessage textMessage;
        private VideoMessage videoMessage;
        private AudioMessage audioMessage;


        //Class constructor, do not edit. Use form load event for initialization
        public Main()
        {
            InitializeComponent();
        }

        //Event handlers

        //Runs immediately after form loads
        private void Main_Load(object sender, EventArgs e)
        {
            //Hide media player until a preview is needed
            playerMain.Visible = false;

            //Add test IP to address combo box for presentation
            cboAddresses.Items.Add("Coleman");
            cboAddresses.Items.Add("Mitchell ");
            cboAddresses.Items.Add("Tyler");
            cboAddresses.Items.Add("Victor");

            //store compose symbol and display on btnCompose
            int i = 11036;
            char c = (char)i;
            btnCompose.Text = c.ToString();

            //store message symbol and display on btnMessages
            int j = 0x00002709;
            char msgChar = (char)j;
            btnMessages.Text = msgChar.ToString();

            //store send symbol and display on btnSend
            int k = 8628;
            char sndChar = (char)k;
            btnSend.Text = sndChar.ToString();
        }
        private void btnSend_Click(object sender, EventArgs e)
        {
            message = new SE_Semester_Project.Message();

            //Store message subtypes
            if(txtOutgoing.Text != "")
            {
                textMessage = new TextMessage(txtOutgoing.Text);

                //store in message object
                message.AddTextMessage(textMessage);
            }

            if(cboFileList.SelectedIndex > -1)
            {
                //Check file extension from selectedFile and store in the appropriate object
                if(selectedFile.Contains("mp3") || selectedFile.Contains(".wav"))
                {
                    audioMessage = new AudioMessage(selectedFile);

                    //Store in message object
                    message.AddAudioMessage(audioMessage);
                }
                else if(selectedFile.Contains("mp4"))
                {
                    videoMessage = new VideoMessage(selectedFile);

                    //Store in message object
                    message.AddVideoMessage(videoMessage);
                }
            }



            //Display simple message for testing
            MessageBox.Show("Message Delivered");

        }
        private void btnStartTime_Click(object sender, EventArgs e)
        {
            //Prompt the user to select start time and store locally
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            //Create an OpenFileDialog object which provides file browser functionality
            OpenFileDialog browser = new OpenFileDialog();
         

            //If a file is selected, result == Dialgoue.OK, otherwise result == Dialogue.Cancel
            DialogResult result = browser.ShowDialog();

            //store entire path in a String
            String path = browser.FileName;

            //Trim path to only include filename and extension
            String pathTrimmed = Path.GetFileName(path);

            //If a file is selected, store the full path into class List<> and add trimmed path to cboFileList
            if (result == DialogResult.OK)
            {
                outgoingFilepaths.Add(path);
                cboFileList.Items.Add(pathTrimmed);
            }
        }

        private void btnCompose_Click(object sender, EventArgs e)
        {
            //Enable composition controls
            txtOutgoing.Enabled = true;
            btnUpload.Enabled = true;
            cboFileList.Enabled = true;

            //Change file combo box and file browser button visibility to true
            cboFileList.Visible = true;
            btnUpload.Visible = true;
        }

        private void btnMessages_Click(object sender, EventArgs e)
        {
            //Create new Messages form, display, and hide the current form
            Messages frmMessages = new Messages();
            frmMessages.Show();
            frmMessages.Location = this.Location;
            this.Hide();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            //Display start time dialog
            StartTimeDialog frmStartTimeDialog = new StartTimeDialog();
            frmStartTimeDialog.Show();
        }


        private void btnDuration_Click(object sender, EventArgs e)
        {
            //Display EndTimeDialog
            EndTimeDialog frmEndTimeDialog = new EndTimeDialog();
            frmEndTimeDialog.Show();
        }

        private void cboAddresses_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Store selected address in a string variable
            selectedAddress = cboAddresses.SelectedItem.ToString();
        }

        private void cboFileList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Display media player for preview if audio or video file is selected.
            selectedFile = cboFileList.SelectedItem.ToString();

            if(selectedFile == "test.wav" || selectedFile == "test.mp3")
            {
                playerMain.Visible = true;
            }
            else
            {
                playerMain.Visible = false;
            }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            //Create new Login form, display, and hide the current form
            Login frmLogin = new Login();
            frmLogin.Show();
            this.Hide();
        }
        
        //Getters
        public string getSelectedAddress()
        {
            return selectedAddress;
        }

        public string getSelectedFile()
        {
            return selectedFile;
        }

        public List<string> getOutgoingFilePaths()
        {
            return outgoingFilepaths;
        }

        public SE_Semester_Project.Message getMessage()
        {
            return message;
        }
    }
}
